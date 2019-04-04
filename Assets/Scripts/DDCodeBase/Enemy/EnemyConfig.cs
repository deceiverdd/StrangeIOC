using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyConfig : ScriptableObject
{
    [UnityEngine.SerializeField]
    public List<EnemyStatus> MonsterStatusList = new List<EnemyStatus>();
}

[System.Serializable]
public class EnemyStatus
{
    public int enemyID;
    public string enemyName;
    public bool isBoss = false;
    public int monsterNum = 0;
    public double hp = 0f;
    public double score = 0f;
    public double monsterVolume = 1;
    public bool isHaveDeathAni;
    public double speed;
    public string musicEffectName;

    public EnemyStatus()
    {

    }

    public EnemyStatus(int enemyID, string enemyName, bool isBoss, int monsterNum, double hp, double score, double monsterVolume,
        bool isHaveDeathAni, double speed, string musicEffectName)
    {
        this.enemyID = enemyID;
        this.enemyName = enemyName;
        this.isBoss = isBoss;
        this.monsterNum = monsterNum;
        this.hp = hp;
        this.score = score;
        this.monsterVolume = monsterVolume;
        this.isHaveDeathAni = isHaveDeathAni;
        this.speed = speed;
        this.musicEffectName = musicEffectName;
    }
}

