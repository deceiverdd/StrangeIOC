using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    private bool isRotateSelf = false;
    private float rotateSelfSpeed = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //自身旋转
        this.RotateSelf();
    }

    void RotateSelf()
    {
        if (!isRotateSelf)
            return;

        this.transform.Rotate(Vector3.right * Time.deltaTime * this.rotateSelfSpeed);
    }

    /// <summary>
    /// 开启自身旋转
    /// </summary>
    /// <param name="rotateSpd"></param>
    public void StartRotateSelf(float rotateSpd = 1f)
    {
        this.rotateSelfSpeed = rotateSpd;
        this.isRotateSelf = true;
    }

    /// <summary>
    /// 关闭自身旋转
    /// </summary>
    public void StopRotateSelf()
    {
        this.isRotateSelf = false;
    }

    /// <summary>
    /// 取得旋转状态
    /// </summary>
    /// <returns></returns>
    public bool GetRotateSelfState()
    {
        return isRotateSelf;
    }

    public void OpenLight()
    {
        if (this.transform.GetComponent<Light>())
            this.transform.GetComponent<Light>().enabled = true;
    }

    public void CloseLight()
    {
        if (this.transform.GetComponent<Light>())
            this.transform.GetComponent<Light>().enabled = false;
    }
}
