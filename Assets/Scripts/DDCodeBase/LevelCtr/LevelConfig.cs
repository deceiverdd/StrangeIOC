using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [UnityEngine.SerializeField]
    public List<LevelStatus> LevelStatusList = new List<LevelStatus>();
}

[System.Serializable]
public class LevelStatus
{
    public int levelID;
    public string levelName;
    public double levelTotalTime;

    public string soldierNum;
    public string rocketManNum;
    public string tankNum;
    public string airPlaneNum;

    public LevelStatus()
    {

    }

    public LevelStatus(int levelid,string levelname, double leveltotaltime, string soldierNum, string rocketManNum,
        string tankNum, string airPlaneNum)
    {
        this.levelID = levelid;
        this.levelName = levelname;
        this.levelTotalTime = leveltotaltime;
        this.soldierNum = soldierNum;
        this.rocketManNum = rocketManNum;
        this.tankNum = tankNum;
        this.airPlaneNum = airPlaneNum;
    }
}

