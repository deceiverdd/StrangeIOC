using NetWorkerManager;
using NES = NetWorkerManager.NetEventSender;
using LitJson;
using UnityEngine;
using System.Collections;

public class MessageCentre : MonoSingleton<MessageCentre>
{
    public static bool isConnected = false;
    public static ConnectState CurrentConnectState;

    private NetWorker NetWorker;
    private MyTcpClient curTcpClient;
    private string serverIP;
    private int serverPort;
    private static string ConnectData;
    private float ReConnectCoolDownTime = 3f;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        DebugLogInUI.Instance.ShowLog("正在连接到服务器...");

        this.ConnectToSever();

        StartCoroutine(HeartBeat());
    }

    // Update is called once per frame
    void Update()
    {
        this.ReConnectCoolDown();
    }

    //定时发送心跳包，断开重连
    IEnumerator HeartBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            DebugLogInUI.Instance.ShowLog("------心跳包------");
            NetWorker.TcpSend("heart beat", curTcpClient);
        }
    }

    private void ReConnectCoolDown()
    {
        this.ReConnectCoolDownTime -= Time.deltaTime;
    }

    public bool SendMessage2Svr(string data)
    {
        if (curTcpClient != null && isConnected)
        {
            NetWorker.TcpSend(data, curTcpClient);
            return true;
        }
        else
        {
            return false;
        }
    }

    //启动软件自动连接到服务器
    private void ConnectToSever()
    {
        serverIP = GameConfig.Instance.ServerIP;
        serverPort = int.Parse(GameConfig.Instance.SeverPort);

        DebugLogInUI.Instance.ShowLog("目标服务器IP：" + serverIP);
        DebugLogInUI.Instance.ShowLog("目标服务器端口：" + serverPort);

        CommandSys commandSys = new CommandSys(NetWorkCommand.Access, GameConfig.Instance.SoftName, null, null);

        ConnectData = JsonIO.LoadObjToJson<CommandSys>(commandSys);

        NetWorker = new NetWorker();
        NetWorker.netWorker2Main += ProcessNetEvent;

        curTcpClient = NetWorker.CreateTcpSend(serverIP, serverPort, ConnectData);
        curTcpClient.closed = false;
    }

    /// <summary>
    /// 处理网络事件
    /// </summary>
    /// <param name="EA"></param>
    private void ProcessNetEvent(NetWorkerEventArgs EA)
    {
        if (curTcpClient.closed)
            return;

        switch (EA.es)
        {
            case NES.Debug: DebugLog(EA.logType, EA.debugLogInfo, EA.eventTime); break;
            case NES.TcpDataReceived: ProcessTcpData(EA); break;
            case NES.TcpClientAccess: break;
            case NES.UdpClientAccess: break;
            case NES.ExceptionReport: DebugLog(NetWorkerManager.LogType.Error, EA.ex.ToString(), System.DateTime.Now); break;
            case NES.TcpClientLost: TcpClientLost(EA.tcpClient); break;
            case NES.TcpConnectFail: ConnectFail(); break;
            case NES.TcpConnectSuccess: ConnectSuccess(); break;
        }
    }

    private void DebugLog(NetWorkerManager.LogType nwed, string info, System.DateTime dateTime)
    {

    }

    private void TcpClientLost(MyTcpClient myTcpClient)
    {
        //连接丢失处理
        this.AutoReConnect();
    }

    /// <summary>
    /// 自动重连
    /// </summary>
    private void AutoReConnect()
    {
        if (curTcpClient.closed)
            return;

        if (this.ReConnectCoolDownTime > 0)
            return;

        this.ReConnectCoolDownTime = 3f;

        DebugLogInUI.Instance.ShowErrorLog("连接丢失，正在重连至服务器");

        //初始化TCP客户端
        this.curTcpClient = null;

        curTcpClient = NetWorker.CreateTcpSend(serverIP, serverPort, ConnectData);
    }

    private void ProcessTcpData(NetWorkerEventArgs EA)
    {
        string datas;

        if (EA.tcpClient.recData.TryDequeue(out datas))
        {
            CommandSys cmdSys = JsonIO.LoadJsonToObj<CommandSys>(datas);

            switch (cmdSys.cmd)
            {
                case NetWorkCommand.GamePlay:
                    //StartGame();//接收到开始游戏的信号
                    break;
                case NetWorkCommand.GameStop:
                    break;
                case NetWorkCommand.UserIntegral:
                    break;
                case NetWorkCommand.UserInfo:
                    break;
                case NetWorkCommand.GetUserInfo:
                    break;
                case NetWorkCommand.Notice:
                    break;
            }
        }
    }

    private void ConnectSuccess()
    {
        isConnected = true;
        DebugLogInUI.Instance.ShowLog("连接到服务器！");
    }

    private void ConnectFail()
    {
        isConnected = false;
        DebugLogInUI.Instance.ShowErrorLog("连接失败！");
    }

    private void OnDestroy()
    {
        curTcpClient.closed = true;
    }

    private void OnApplicationQuit()
    {
        curTcpClient.closed = true;
        curTcpClient.Close();
    }
}
