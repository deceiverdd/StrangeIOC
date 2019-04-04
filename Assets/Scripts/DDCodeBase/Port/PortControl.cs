//#define RECEIVESINGLEBYTE
#define RECEIVEBUFFERBYTE

using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class PortControl : Singleton<PortControl>
{
    [Tooltip("串口名")]
    public string PortName = "COM1";
    [Tooltip("波特率")]
    public int BaudRate = 115200;
    [Tooltip("校验位")]
    public Parity Parity = Parity.None;
    [Tooltip("数据位")]
    public int DataBits = 8;
    [Tooltip("停止位")]
    public StopBits StopBits = StopBits.One;

    public List<byte> ListReceive = new List<byte>();

    private SerialPort sp = null;
    private Thread dataReceiveThread;

    public bool InitPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
    {
        return OpenPort(portName,baudRate,parity,dataBits,stopBits);

        //串口通信接收回调
        //dataReceiveThread = new Thread(new ThreadStart(DataReceiveFunction));
        //dataReceiveThread.Start();
    }

    /// <summary>
    /// 创建串口并打开串口
    /// </summary>
    private bool OpenPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
    {
        //string[] ArrayPort = SerialPort.GetPortNames();
        //foreach (var p in ArrayPort)
        //{
        //    Debuger.Log(p);
        //}
        //PortName = ArrayPort[0];

        this.PortName = portName;
        this.BaudRate = baudRate;
        this.Parity = parity;
        this.DataBits = dataBits;
        this.StopBits = stopBits;

        sp = new SerialPort(PortName, BaudRate, Parity,DataBits,StopBits);
        sp.ReadTimeout = 500;

        try
        {
            sp.Open();
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    void OnApplicationQuit()
    {
        ClosePort();
    }

    public void ClosePort()
    {
        try
        {
            sp.Close();
            dataReceiveThread.Abort();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 接收数据
    /// </summary>
    private void DataReceiveFunction()
    {

#if RECEIVESINGLEBYTE//按单个字节处理信息，不能接收中文
        while (sp != null && sp.IsOpen)
        {
            Thread.Sleep(1);

            try
            {
                byte addr = Convert.ToByte(sp.ReadByte());
                sp.DiscardInBuffer();
                //DiscardInBuffer方法清除串行驱动程序的接收缓冲区的数据；
                //DiscardOutBuffer方法清除串行驱动程序的发送缓冲区的数据
                ListReceive.Add(addr);

            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                ListReceive.Clear();
            }
        }
#elif RECEIVEBUFFERBYTE//按字节数组处理信息，信息缺失
        byte[] buffer = new byte[1024];
        int bytes = 0;
        while (sp != null & sp.IsOpen)
        {
            try
            {
                bytes = sp.Read(buffer, 0, buffer.Length);
                if (bytes == 0)
                {
                    continue;
                }
                else
                {
                    string strbytes = Encoding.Default.GetString(buffer);
                }
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(ThreadAbortException))
                {

                }
            }

            Thread.Sleep(10);
        }
#endif
    }

    /// <summary>
    /// 将指定字符串写入串口
    /// </summary>
    /// <param name="dataStr"></param>
    public void WriteData(string dataStr)
    {
        if (sp.IsOpen)
        {
            sp.Write(dataStr);
        }
    }

    /// <summary>
    /// 将指定数量的字节写入串行端口
    /// </summary>
    /// <param name="buffer">包含要写入端口的数据的字节数组</param>
    /// <param name="offset">参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
    /// <param name="count">要写入的字节数</param>
    public void WriteData(byte[] buffer, int offset, int count)
    {
        if (sp.IsOpen)
        {
            sp.Write(buffer, offset, count);
        }
    }

    /// <summary>
    /// 使用缓冲区中的数据将指定数量的字符写入串行端口
    /// </summary>
    /// <param name="buffer">包含要写入端口的数据的字符数组</param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    public void WriteData(char[] buffer, int offset, int count)
    {
        if (sp.IsOpen)
        {
            sp.Write(buffer, offset, count);
        }
    }
}


