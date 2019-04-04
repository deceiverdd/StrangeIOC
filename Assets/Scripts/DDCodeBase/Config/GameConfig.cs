using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoSingleton<GameConfig>
{
    public bool isDebugMode;
    public string ServerIP;
    public string SeverPort;
    public string SoftName;
    public EnemyConfig curEnemyConfig;
    public LevelConfig curLevelConfig;
    public double curScore = 0f;
    public int curLevel = 0;

    public FloatVariable player0score;
    public FloatVariable player1score;
    public FloatVariable player2score;
    public FloatVariable player3score;
    public FloatVariable player4score;
    public FloatVariable player5score;

    public string PortName = "COM1";
    public int BaudRate = 9600;
    public int Parity = 0;
    public int DataBits = 8;
    public int StopBits = 1;

    protected override void Awake()
    {
        base.Awake();
        ConfigRead.Instance.ReadConfig();
        this.InitDebugMode();

        //this.SpecialHandle();
    }

    /// <summary>
    /// 不同项目的特殊处理
    /// </summary>
    private void SpecialHandle()
    {
        DisplayCheck();
        DataConfigLoad();
    }

    /// <summary>
    /// DebugMode配置
    /// </summary>
    private void InitDebugMode()
    {
        if (isDebugMode == true)
        {
            Cursor.visible = true;
            Debuger.EnableLog = true;
            SRDebug.Init();
        }
        else
        {
            Cursor.visible = false;
            Debuger.EnableLog = false;
        }
    }

    /// <summary>
    /// 分屏显示
    /// </summary>
    private void DisplayCheck()
    {
        int displayLength = Display.displays.Length;

        Debuger.Log("displays connected: " + displayLength);
        // Display.displays[0] 是主显示器, 默认显示并始终在主显示器上显示.        
        // 检查其他显示器是否可用并激活. 

        if (displayLength > 1)
            Display.displays[1].Activate();
        if (displayLength > 2)
            Display.displays[2].Activate();
        if (displayLength > 3)
            Display.displays[3].Activate();
        if (displayLength > 4)
            Display.displays[4].Activate();
        if (displayLength > 5)
            Display.displays[5].Activate();
    }

    /// <summary>
    /// JSON数据加载
    /// </summary>
    private void DataConfigLoad()
    {
        this.curEnemyConfig.MonsterStatusList = JsonIO.LoadJsonFromFile<EnemyConfig>("EnemyData").MonsterStatusList;
        this.curLevelConfig.LevelStatusList = JsonIO.LoadJsonFromFile<LevelConfig>("LevelData").LevelStatusList;
    }
}
