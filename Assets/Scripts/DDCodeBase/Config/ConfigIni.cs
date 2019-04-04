using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

/// <summary>
/// 读取ini配置文件
/// [Time] 
/// time=10 
/// [Speed] 
/// speed=5 
/// ConfigIni ini=new ConfigIni(Application.StreamingAssets+"/Setting.ini"); 
/// time=ini.ReadIniContent("Time","time");
/// speed=ini.ReadIniContent("Speed","speed");
/// ini.WritePrivateProfileString("Count","count","5");
/// </summary>
public class ConfigIni
{
    public string path;
    public Dictionary<string, string> keyVal = new Dictionary<string, string>();
    public List<ConfigData> ListConfigData = new List<ConfigData>();

    [DllImport("kernel32")]
    public static extern long WritePrivateProfileString(string section, string key, string value, string path);
    [DllImport("kernel32")]
    public static extern int GetPrivateProfileString(string section, string key, string deval, StringBuilder stringBuilder, int size, string path);
    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title, int type);

    public ConfigIni(string path)
    {
        try
        {
            this.path = path;

            StreamReader sr = new StreamReader(path, Encoding.Default);

            string line;
            string section = "Default";

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("["))
                {
                    section = line.Trim('[').Trim(']');
                }

                if (line.Contains("="))
                {
                    string[] kv = line.Split('=');
                    string key = kv[0].Trim();
                    string v = kv[1].Trim();
                    keyVal.Add(key, v);

                    ListConfigData.Add(new ConfigData(section, key, v));
                    section = "Default";
                }
            }
        }
        catch (Exception)
        {
            Debuger.Log("配置文件:" + path + " 读取错误!");
        }       
    }

    private void WriteIniContent(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, this.path);
    }

    /// <summary>
    /// 读取Ini文件
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public string ReadIniContent(string section, string key)
    {
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(section, key, "", temp, 255, this.path);
        //MessageBox(IntPtr.Zero, this.path+i + ","+temp+","+section+key, "ReadIniContent", 0);
        return temp.ToString();
    }

    /// <summary>
    /// 写入Ini文件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void WriteConfigData(string key, string value)
    {
        if(IsIniPath())
        {
            File.WriteAllText(path, string.Empty);
        }
        else
        {
            Debuger.Log(path + " not exist!");
            return;
        }

        for (int i = 0; i < ListConfigData.Count; i++)
        {
            if (ListConfigData[i].key == key)
            {
                ListConfigData[i].ModifyValue(value);
            }
        }

        for (int i = 0; i < ListConfigData.Count; i++)
        {
            ConfigData configData = ListConfigData[i];
            WriteIniContent(configData.section, configData.key, configData.value);
        }

        Debuger.Log("Write " + path + " successful!");
    }

    /// <summary>
    /// 判断路径是否正确
    /// </summary>
    /// <returns></returns>
    public bool IsIniPath()
    {
        return File.Exists(this.path);
    }
}

public class ConfigData
{
    public string section;
    public string key;
    public string value;

    public ConfigData(string section, string key, string value)
    {
        this.section = section;
        this.key = key;
        this.value = value;
    }

    public void ModifyValue(string value)
    {
        this.value = value;
    }
}

