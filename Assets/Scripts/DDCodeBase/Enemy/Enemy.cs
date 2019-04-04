using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ID;
    public bool isDeath = false;
    public string enemyName;

    [SerializeField] private double maxLife;
    [SerializeField] private double curLife;
    [SerializeField] private double speed;
    [SerializeField] private double score;
    [SerializeField] private bool isBoss = false;

    protected virtual void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        this.enemyName = GameConfig.Instance.curEnemyConfig.MonsterStatusList[this.ID].enemyName;
        this.maxLife = GameConfig.Instance.curEnemyConfig.MonsterStatusList[this.ID].hp;
        this.speed = GameConfig.Instance.curEnemyConfig.MonsterStatusList[this.ID].speed;
        this.score = GameConfig.Instance.curEnemyConfig.MonsterStatusList[this.ID].score;
        this.isBoss = GameConfig.Instance.curEnemyConfig.MonsterStatusList[this.ID].isBoss;
    }

    public void ReceiveDamage(float damage)
    {
        if (this.curLife <= 0)
            return;

        this.curLife -= damage;

        if(this.curLife <= 0)
        {
            this.isDeath = true;
            //Add Score
        }
    }
}
