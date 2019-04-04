using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4DataReceived
{
    /// <summary>
    /// 收到的数据包，处理数据
    /// </summary>
    /// <param name="data"></param>
    public static List<MyPoint> DataRec(string data)
    {
        // 解析指令包
        CommandSys cmdSys = JsonConvert.DeserializeObject<CommandSys>(data);
        // 解析数据包
        List<MyPoint> mps = JsonConvert.DeserializeObject<List<MyPoint>>(cmdSys.value);

        return mps;
    }

    /// <summary>
    /// 物理坐标与屏幕坐标信息集合
    /// </summary>
    public class MyPoint
    {
        /// <summary>
        /// 物理中心点坐标
        /// </summary>
        public double[] rCP;
        /// <summary>
        /// 物理屏幕坐标云点
        /// </summary>
        [JsonIgnore]
        public List<double[]> rP = new List<double[]>();
        /// <summary>
        /// 屏幕中心点坐标
        /// </summary>
        public Point sCP = new Point(0, 0);
        /// <summary>
        /// 屏幕云点坐标
        /// </summary>
        public List<Point> sP = new List<Point>();
        /// <summary>
        /// 坐标追踪
        /// </summary>
        public int index = -1;
        /// <summary>
        /// 时间戳
        /// </summary>
        public long pointTick = 0;

    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum Command
    {
        CenterPointData,
        PointData
    }

    /// <summary>
    /// 指令系统数据包
    /// </summary>
    public class CommandSys
    {
        /// <summary>
        /// 指令
        /// </summary>
        public Command cmd;
        public string id = null;
        /// <summary>
        /// 数据包
        /// </summary>
        public string value = null;
    }
}


