using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstField
{
    public const string LEVEL0SCENENAME = "StandbyScene";
    public const string LEVEL1SCENENAME = "VRUniverseRoam_DynamicSeat";

    public enum SceneName
    {
        StandbyScene,
        VRUniverseRoam_DynamicSeat
    }

    public static string EnumConvertToString(SceneName name)
    {
        //方法一
        //return name.ToString();

        //方法二
        return SceneName.GetName(name.GetType(), name);
    }
}
