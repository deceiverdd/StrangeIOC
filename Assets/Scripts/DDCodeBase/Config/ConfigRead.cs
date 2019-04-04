using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO.Ports;

public class ConfigRead : Singleton<ConfigRead>
{
    public const string KeyIsDebugMode = "isDebugMode";
    public const string KeyServerIP = "ServerIP";
    public const string KeyServerPort = "ServerPort";
    public const string KeySoftName = "SoftName";
    public const string KeyIsMouseControl = "isMouseControl";
    public const string KeyEyesDistance = "EyesDistance";

    public ConfigIni ini = null;

    public void Init()
    {
        //从配置文件读取
#if !UNITY_EDITOR
        //打包好的“xxx_Data”目录没有读取里面的文件权限
        //所以对于打包的程序，需要把配置文件config.ini放在exe同目录下
        string configFile = System.Environment.CurrentDirectory + "/config.ini";
#else
        string configFile = Application.dataPath + "/config.ini";
#endif

        if (File.Exists(configFile))
        {
            ini = new ConfigIni(configFile);
        }
    }

    /// <summary>
    /// 读取配置文件
    /// </summary>
    public void ReadConfig()
    {
        if(ini == null)
        {
            Init();
        }

        //example
        //GameConfig.Instance.isDebugMode = bool.Parse(ini.keyVal["isDebugMode"]);

        GameConfig.Instance.isDebugMode = bool.Parse(ini.keyVal[KeyIsDebugMode]);
    }

    /// <summary>
    /// 修改并保存配置文件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveConfigData(string key, string value)
    {
        ini.WriteConfigData(key, value);
    }
}


