using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public static class ToolKit
{
    /// <summary>
    /// 获取指定区间的随机数
    /// </summary>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    /// <returns></returns>
    public static int GetRandomInt(int min = 0, int max = 100)
    {
        return new System.Random(GetRandomSeed()).Next(min, max);
    }

    public static float GetRandomFloat(int min = 0, int max = 100)
    {
        return new System.Random(GetRandomSeed()).Next(min, max);
    }

    /// <summary>
    /// 加密随机数生成器 生成随机种子
    /// </summary>
    /// <returns></returns>
    static int GetRandomSeed()
    {

        byte[] bytes = new byte[4];

        System.Security.Cryptography.RNGCryptoServiceProvider r = new System.Security.Cryptography.RNGCryptoServiceProvider();

        r.GetBytes(bytes);

        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// 将byte数组转成十六进制FF FF类型字符串输出
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string BytesConvertToStr(byte[] bytes)
    {
        String str = null;

        foreach (var B in bytes)
        {
            str = str + Convert.ToString(B, 16) + " ";
        }

        Debuger.Log(str);
        return str;
    }

    /// <summary>
    /// 将byte数组转成十六进制FF_FF类型字符串输出
    /// </summary>
    /// <param name="bytes"></param>
    public static void OutputDataPackage(byte[] bytes)
    {
        Debuger.Log(BitConverter.ToString(bytes, 0));
    }

    /// <summary>
    /// 将FF_FF类型字符串转成byte数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] StrConvertToBytes(string str)
    {
        str = str.Replace("-", "");
        if((str.Length%2)!=0)
        {
            str = str.Insert(str.Length - 1, 0.ToString());
        }
        byte[] returnBytes = new byte[str.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
        {
            returnBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
        }
        return returnBytes;
    }

    /// <summary>
    /// 将字节数组从指定索引开始转成float输出
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex"></param>
    public static void OutputBytesToFloatData(byte[] bytes, int startIndex)
    {
        float data = BitConverter.ToSingle(bytes, startIndex);

        Debuger.Log(data);
    }

    public static void UIDebug(string str)
    {
        DebugLogInUI.Instance.ShowLog(str);
    }

    public static void UIErrorDebug(string str)
    {
        DebugLogInUI.Instance.ShowErrorLog(str);
    }

    /// <summary>
    /// 输出中文日期
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    public static string Baodate2Chinese(string strDate)
    {
        char[] strChinese = new char[] {
                 '〇','一','二','三','四','五','六','七','八','九','十'
             };
        StringBuilder result = new StringBuilder();

        //// 依据正则表达式判断参数是否正确
        //Regex theReg = new Regex(@"(d{2}|d{4})(/|-)(d{1,2})(/|-)(d{1,2})");

        if (!string.IsNullOrEmpty(strDate))
        {
            // 将数字日期的年月日存到字符数组str中
            string[] str = null;
            if (strDate.Contains("-"))
            {
                str = strDate.Split('-');
            }
            else if (strDate.Contains("/"))
            {
                str = strDate.Split('/');
            }
            // str[0]中为年，将其各个字符转换为相应的汉字
            for (int i = 0; i < str[0].Length; i++)
            {
                result.Append(strChinese[int.Parse(str[0][i].ToString())]);
            }
            result.Append("年");

            // 转换月
            int month = int.Parse(str[1]);
            int MN1 = month / 10;
            int MN2 = month % 10;

            if (MN1 > 1)
            {
                result.Append(strChinese[MN1]);
            }
            if (MN1 > 0)
            {
                result.Append(strChinese[10]);
            }
            if (MN2 != 0)
            {
                result.Append(strChinese[MN2]);
            }
            result.Append("月");

            // 转换日
            int day = int.Parse(str[2]);
            int DN1 = day / 10;
            int DN2 = day % 10;

            if (DN1 > 1)
            {
                result.Append(strChinese[DN1]);
            }
            if (DN1 > 0)
            {
                result.Append(strChinese[10]);
            }
            if (DN2 != 0)
            {
                result.Append(strChinese[DN2]);
            }
            result.Append("日");
        }
        else
        {
            throw new ArgumentException();
        }

        return result.ToString();
    }

    /// <summary>
    /// GZip压缩字节数组
    /// </summary>
    /// <param name="str"></param>
    public static byte[] Compress(byte[] inputBytes)
    {
        using (MemoryStream outStream = new MemoryStream())
        {
            using (GZipStream zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {
                zipStream.Write(inputBytes, 0, inputBytes.Length);
                zipStream.Close(); //很重要，必须关闭，否则无法正确解压
                return outStream.ToArray();
            }
        }
    }

    /// <summary>
    /// GZip解压缩字节数组
    /// </summary>
    /// <param name="str"></param>
    public static byte[] Decompress(byte[] inputBytes)
    {
        using (MemoryStream inputStream = new MemoryStream(inputBytes))
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    zipStream.CopyTo(outStream);
                    zipStream.Close();
                    return outStream.ToArray();
                }
            }

        }
    }
}
