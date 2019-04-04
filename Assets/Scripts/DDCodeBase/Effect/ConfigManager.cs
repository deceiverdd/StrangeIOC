using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;

public class ConfigManager : MonoSingleton<ConfigManager>
{
    protected override void Awake()
    {
        base.Awake();
        confEffectManager.datas = Load<conf_effect>();
    }

    public static class confEffectManager
    {
        public static List<conf_effect> datas;

        public static conf_effect GetData(int effectid)
        {
            foreach(conf_effect e in datas)
            {
                if (e.id == effectid)
                    return e;
            }

            return null;
        }       
    }

    public static List<T> Load<T>() where T : new()
    {
        //这里要注意配置对象要与配置文件名称相同
        string[] names = (typeof(T)).ToString().Split('.');

        //获取真实名称
        string filename = names[names.Length - 1];
        XmlDocument doc = new XmlDocument();
        //加载xml文件
        TextAsset data = (TextAsset)Resources.Load("Config/" + filename);

        doc.LoadXml(data.text);
        XmlNode xmlNode = doc.DocumentElement;

        XmlNodeList xnl = xmlNode.ChildNodes;

        List<T> ret = new List<T>();

        //遍历所有内容
        foreach (XmlNode xn in xnl)
        {
            //找到符合条件的数据
            if (xn.Name.ToLower() == filename)
            {
                //实例化数据对象
                T obj = new T();

                Type t = obj.GetType();

                //获取对象的全部属性
                FieldInfo[] fields = t.GetFields();

                string msg = "";
                try
                {
                    //遍历全部属性，并从配置表中找出对应字段的数据并赋值
                    //根据属性的类型对数据进行转换
                    foreach (FieldInfo field in fields)
                    {
                        if (xn.Attributes[field.Name] == null)
                        {
                            Debug.Log("the field [" + field.Name + "] is null !!!");
                            continue;
                        }
                        string val = xn.Attributes[field.Name].Value;
                        if (val == null)
                        {
                            Debug.Log("the field [" + field.Name + "] is null !!!");
                            continue;
                        }

                        msg = field.Name + " : " + val + "   type : " + field.FieldType;
                        if (field.FieldType == typeof(int))
                        {
                            field.SetValue(obj, int.Parse(val));
                        }
                        else if (field.FieldType == typeof(float))
                        {
                            field.SetValue(obj, float.Parse(val));
                        }
                        else if (field.FieldType == typeof(string))
                        {
                            field.SetValue(obj, val);
                        }

                    }
                    ret.Add(obj);
                }
                catch (Exception e)
                {
                    Debug.LogError("=====================" + filename + "==================");
                    Debug.LogError(e.Message);
                    Debug.LogError(msg);
                }

            }
        }

        return ret;

    }

}