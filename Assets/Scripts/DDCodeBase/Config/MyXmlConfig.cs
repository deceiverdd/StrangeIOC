using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MyXmlConfig
{
    public const string ConfigFileName = "config.xml";

    // 类方法：
    public static void TestString(int portNum)
    {
        string err = "The port number[0] invalid";
        Debug.Log("Construct MySerialComm. ");
        if (portNum < 1 || portNum > 9)
        {
            Debug.Log(err.Replace("0", portNum.ToString()));
        }
    }

    /// <summary>
    /// 读取整数配置
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="defaultInt"></param>
    /// <returns></returns>
    public static int ConfigReadInt(string tag, int defaultInt = 0)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
        XmlElement rootElem = doc.DocumentElement;
        XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
        if (personNodes.Count == 1)
        {
            return int.Parse(personNodes[0].InnerText);
        }
        return defaultInt;
    }

    /// <summary>
    /// 读取布尔类型配置
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="defaultBool"></param>
    /// <returns></returns>
    public static bool ConfigReadBool(string tag, bool defaultBool = false)
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
            XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
            if (personNodes.Count == 1)
            {
                if (personNodes[0].InnerText == "0")
                    return false;
                else if (personNodes[0].InnerText == "1")
                    return true;
                return bool.Parse(personNodes[0].InnerText);
            }
        }
        catch
        {
            Debug.Log("Error: config.xml not exist.");
        }
        return defaultBool;
    }

    /// <summary>
    /// 读取浮点数配置
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="defaultFloat"></param>
    /// <returns></returns>
    public static float ConfigReadFloat(string tag, float defaultFloat = 0)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
        XmlElement rootElem = doc.DocumentElement;
        XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
        if (personNodes.Count == 1)
        {
            return float.Parse(personNodes[0].InnerText);
        }
        return defaultFloat;
    }

    /// <summary>
    /// 读取字符串配置
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="defaultStr"></param>
    /// <returns></returns>
    public static string ConfigReadString(string tag, string defaultStr = "")
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
            XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
            if (personNodes.Count == 1)
            {
                return personNodes[0].InnerText;
            }
        }
        catch
        {
            Debug.Log("Error: config.xml not exist.");
        }
        return defaultStr;

    }

    /// <summary>
    /// 读取字符串列表配置
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static List<string> ConfigReadStringArray(string tag)
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
            XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
            if (personNodes.Count == 1)
            {
                XmlElement _province = (XmlElement)personNodes[0];
                if (_province.ChildNodes.Count < 1)
                    return null;
                List<string> strList = new List<string>(_province.ChildNodes.Count);
                foreach (XmlElement childN in _province.ChildNodes)
                {
                    strList.Add(childN.InnerText);
                }
                return strList;
            }
        }
        catch
        {
            Debug.Log("Error: config.xml not exist.");
        }
        return null;

    }

    /// <summary>
    /// 读取浮点数列表配置
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static List<float> ConfigReadFloatArray(string tag)
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
            XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
            if (personNodes.Count == 1)
            {
                XmlElement _province = (XmlElement)personNodes[0];
                if (_province.ChildNodes.Count < 1)
                    return null;
                List<float> floatList = new List<float>(_province.ChildNodes.Count);
                foreach (XmlElement childN in _province.ChildNodes)
                {
                    floatList.Add(float.Parse(childN.InnerText));
                }
                return floatList;
            }
        }
        catch
        {
            Debug.Log("Error: config.xml not exist.");
        }
        return null;

    }

    /// <summary>
    /// 读取Vector3配置
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static Vector3 ConfigReadVector3(string tag)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Application.streamingAssetsPath + "/" + ConfigFileName);
        XmlElement rootElem = doc.DocumentElement;
        XmlNodeList personNodes = rootElem.GetElementsByTagName(tag);
        if (personNodes.Count == 1)
        {
            XmlElement _province = (XmlElement)personNodes[0];
            if (_province.ChildNodes.Count < 3)
                return Vector3.zero;
            List<string> strList = new List<string>(_province.ChildNodes.Count);
            XmlElement xNode = (XmlElement)_province.ChildNodes[0];
            XmlElement yNode = (XmlElement)_province.ChildNodes[1];
            XmlElement zNode = (XmlElement)_province.ChildNodes[2];
            float x = float.Parse(xNode.InnerText);
            float y = float.Parse(yNode.InnerText);
            float z = float.Parse(zNode.InnerText);
            return new Vector3(x, y, z);
        }
        return Vector3.zero;
    }

    //泛型配置
    //public static List<T> ConfigReadTypeArray<T>(string tag)
    //{
    //    List<T> t = new List<T>(3);
    //    if(t.GetType() == typeof(int))
    //    {
    //    }
    //    return t;
    //}
}

