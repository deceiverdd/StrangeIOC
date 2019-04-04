using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Video;

public class SeatActionCollector : MonoBehaviour
{
    //需要采集动作的座椅
    private Transform PlayerSeat;

    [Tooltip("鼠标移动值到座椅偏转的缩放系数")]
    public float Factor = 0.5f;

    public VideoPlayer curVideo;

    private int xDirection = 0;//控制板x轴旋转方向
    private int yDirection = 0;
    private float xFactor = 0f;//控制板x轴旋转系数
    private float yFactor = 0f;

    private bool isShake = false;
    private int ShakeScale = 0;
    private Vector3 offsetPos = Vector3.zero;

    public bool isStartCollect = false;
    public Vector3 oriMousePos = Vector3.zero;
    public Vector3 oriSeatRotation = Vector3.zero;
    private float sendTimeCount = 0.1f;//默认1秒发10次包
    private float timeCount = 0;
    private bool ShakeMainSwitch = true;

    #region 南京全控科技有限公司运动平台控制板_六(三)自由度姿态 X, Y, Z, α, β, γ_通讯协议V135
    //数据包总长度为32位
    private byte[] head = new byte[] { 0xFB };//采用FBFD帧头
    private byte[] protocol1 = new byte[] { 0xFD };
    private byte[] protocol2 = new byte[] { 0xFE };
    //Byte30为震动位，两种震动模式
    private byte[] DefaultParameters = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 };
    private byte[] ShakeParameters_Scale1 = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 };
    private byte[] ShakeParameters_Scale2 = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00 };
    #endregion

    private void Awake()
    {
        Debuger.EnableLog = true;
    }

    // Use this for initialization
    void Start()
    {
        this.PlayerSeat = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12) && !isStartCollect)
        {
            this.isStartCollect = true;
            this.oriMousePos = Input.mousePosition;
            this.curVideo.Play();

            this.oriSeatRotation = this.PlayerSeat.eulerAngles;
            Debug.Log("开始采集！");
        }
        else if(Input.GetKeyDown(KeyCode.F12) && isStartCollect)
        {
            this.isStartCollect = false;

            if (this.curVideo.isPlaying)
                this.curVideo.Stop();

            Debug.Log("结束采集！");
        }
       
        if (!this.isStartCollect)
            return;

        Timer();

        DetectInput();

        NormalShakePlatform();
    }

    void DetectInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.ShakeScale = 1;
            //Debug.Log(ShakeScale);
        }
        else
        {
            this.ShakeScale = 0;
            //Debug.Log(ShakeScale);
        }

        //Debug.Log(Input.mousePosition);

        this.offsetPos = Input.mousePosition - oriMousePos;

        PlayerSeat.eulerAngles = new Vector3(oriSeatRotation.x + Mathf.Clamp(this.offsetPos.y * Factor, -45, 45),
                                             PlayerSeat.eulerAngles.y,
                                             oriSeatRotation.z - Mathf.Clamp(this.offsetPos.x * Factor, -45, 45));
    }

    void Timer()
    {
        timeCount += Time.deltaTime;
        //Debuger.Log(timeCount);

        if (timeCount >= this.sendTimeCount)
        {
            this.isShake = true;
            timeCount = 0f;
        }
    }

    /// <summary>
    /// 动感座椅控制板Control
    /// </summary>
    void NormalShakePlatform()
    {
        if (!isShake)
            return;

        isShake = false;

        //Debug.Log("x : " + PlayerSeat.eulerAngles.x + " , " + "z : " + PlayerSeat.eulerAngles.z);

        if (PlayerSeat.eulerAngles.x > 0f && PlayerSeat.eulerAngles.x < 60f)
        {
            yDirection = -1;

            yFactor = Mathf.Clamp01(PlayerSeat.eulerAngles.x / 30f);

            //Debug.Log("前倾");
        }
        else if (PlayerSeat.eulerAngles.x < 360f && PlayerSeat.eulerAngles.x > 300f)
        {
            yDirection = 1;

            yFactor = Mathf.Clamp01((360f - PlayerSeat.eulerAngles.x) / 30f);

            //Debug.Log("后倾");
        }


        if (PlayerSeat.eulerAngles.z > 0 && PlayerSeat.eulerAngles.z < 60f)
        {
            xDirection = -1;

            xFactor = Mathf.Clamp01(PlayerSeat.eulerAngles.z / 30f);

            //Debug.Log("左倾");
        }
        else if (PlayerSeat.eulerAngles.z < 360f && PlayerSeat.eulerAngles.z > 300f)
        {
            xDirection = 1;

            xFactor = Mathf.Clamp01((360f - PlayerSeat.eulerAngles.z) / 30f);

            //Debug.Log("右倾");
        }

        //控制板三个方向可接受最大旋转角度分别为：x:-18~18，y:-15~15,z:-28~28(z方向在三自由度中不可用)
        //控制板上移至100为水平基准值，上下移动最大范围取0~200，翻滚和俯仰最大角度范围取-14~14，
        PlatformActionControl(true, 0f, 0f, 100f, xFactor * xDirection * 14f, yFactor * yDirection * 14f, 0f, ShakeScale);
    }

    public void PlatformActionControl(bool isProtocol1, float Sway, float Surge, float Heave, float Roll, float Pitch, float Yaw, int ShakeScale = 1)
    {
        byte[] DataPackage;
        byte[] SwayBytes = BitConverter.GetBytes(Sway);
        byte[] SurgeBytes = BitConverter.GetBytes(Surge);
        byte[] HeaveBytes = BitConverter.GetBytes(Heave);
        byte[] RollBytes = BitConverter.GetBytes(Roll);
        byte[] PitchBytes = BitConverter.GetBytes(Pitch);
        byte[] YawBytes = BitConverter.GetBytes(Yaw);
        //byte[] CheckCodeAdd = b.Skip(b.Count() - 8).ToArray();

        if (isProtocol1)
        {
            byte[] checkCodeLink = protocol1.Concat(SwayBytes).ToArray();
            checkCodeLink = checkCodeLink.Concat(SurgeBytes).ToArray();
            checkCodeLink = checkCodeLink.Concat(HeaveBytes).ToArray();
            checkCodeLink = checkCodeLink.Concat(RollBytes).ToArray();
            checkCodeLink = checkCodeLink.Concat(PitchBytes).ToArray();
            checkCodeLink = checkCodeLink.Concat(YawBytes).ToArray();


            //是否开启震动
            if (ShakeMainSwitch)
            {
                //震动等级，0：关闭震动，1：轻微震动，2：剧烈震动
                switch (ShakeScale)
                {
                    case 0:
                        checkCodeLink = checkCodeLink.Concat(DefaultParameters).ToArray();
                        break;
                    case 1:
                        checkCodeLink = checkCodeLink.Concat(ShakeParameters_Scale1).ToArray();
                        break;
                    case 2:
                        checkCodeLink = checkCodeLink.Concat(ShakeParameters_Scale2).ToArray();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                checkCodeLink = checkCodeLink.Concat(DefaultParameters).ToArray();
            }

            //校验和，从第 Byte1 到 Byte30 的和校验
            byte[] checkCode = new byte[] { GetCheckCode(checkCodeLink) };

            DataPackage = head.Concat(checkCodeLink).ToArray();
            DataPackage = DataPackage.Concat(checkCode).ToArray();

            OutputDataPackage(DataPackage);

            //write to text
            //TXTIO.Instance.WriteIntoTxt("DataPackage", BitConverter.ToString(DataPackage, 0));

        }
        else
        {
            UnityEngine.Debug.Log("protocol2");
        }

        //float floatValue = 30f;
        //byte[] bytes = BitConverter.GetBytes(floatValue);

        //foreach (var b in bytes)
        //{
        //    Debug.Log(b);
        //}
    }

    byte GetCheckCode(byte[] bytes)
    {
        int cks = 0;
        foreach (byte item in bytes)
        {
            cks = (cks + item) % 0xffff;
        }

        return (byte)(cks & 0xff);
    }

    void OutputDataPackage(byte[] bytes)
    {
        UnityEngine.Debug.Log(BitConverter.ToString(bytes, 0));
        //DataPackageShow.text = BitConverter.ToString(bytes, 0);
    }
}
