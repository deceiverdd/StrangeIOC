using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;
using System;

//Json读写 -----------------------------------------
public class JsonIO
{
    /// <summary>
    /// Json文件读取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    static public T LoadJsonFromFile<T>(string jsonFileName)
    {
        string _filePath = Application.dataPath + "/Resources/Config/" + jsonFileName + ".json";

#if !UNITY_EDITOR
        _filePath = System.Environment.CurrentDirectory + "/" + jsonFileName + ".json";
#endif

        if (!File.Exists(_filePath))
        {
            Debug.LogWarning("[ 文件不存在! 取消读取操作! ]  文件读取路径 : " + _filePath);
            DebugLogInUI.Instance.ShowErrorLog(_filePath + "文件不存在！");
            return default(T);
        }

        //读取Json文件.
        //根据使用的数据类型<T>值可以是任何类型.相应返回值做一些修改即可.非常方便.
        return JsonMapper.ToObject<T>(new JsonReader(File.ReadAllText(_filePath)));
    }

    /// <summary>
    /// 输出Json文件到指定目录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataList"></param>
    /// <param name="path"></param>
    /// <param name="jsonFileName"></param>
    static public void ExportJsonToFile<T>(T dataList, string path, string jsonFileName)
    {
        string jsonStr = JsonMapper.ToJson(dataList);

        //将json中部分Unicode转成中文
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");//正则表达式规定格式
        jsonStr = reg.Replace(jsonStr,
        delegate (Match m)
        {
            return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
        });

        string fp = path + "/" + jsonFileName + ".json";

        if (!File.Exists(fp)) // 判断是否已有相同文件 
        {
            FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
            fs1.Close();
        }

        File.WriteAllText(fp, jsonStr, System.Text.Encoding.UTF8);
    }

    static public string LoadObjToJson<T>(T obj)
    {
        //读取对象
        //根据使用的数据类型<T>值可以是任何类型.相应返回值做一些修改即可.非常方便.
        return JsonMapper.ToJson(obj);
    }

    static public T LoadJsonToObj<T>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }
}

