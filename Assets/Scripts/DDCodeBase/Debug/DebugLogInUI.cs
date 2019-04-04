using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 在DebugMode模式下，在界面上显示日志调试窗口
/// </summary>
public class DebugLogInUI : MonoSingleton<DebugLogInUI>
{
    private GameObject DebugPanel;
    private Text LogText;
    private Scrollbar VerticalScrollbar;
    private int lineCount = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        this.DeBugModeInit();
    }

    void DeBugModeInit()
    {
        if (!GameConfig.Instance.isDebugMode)
            return;

        this.ShowLog("DebugMode开启");
    }

    /// <summary>
    /// 组件检查
    /// </summary>
    private void ComponentCheck()
    {
        if (!DebugPanel)
        {
            DebugPanel = GameObject.Find("DontDestroyCanvas").transform.Find("DebugLogPanel").gameObject;
            DebugPanel.SetActive(true);

            LogText = DebugPanel.transform.Find("Scroll View/Viewport/Content/Text").GetComponent<Text>();
            VerticalScrollbar = DebugPanel.transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>();
        }
    }

    /// <summary>
    /// 安全性检查
    /// </summary>
    /// <returns></returns>
    private bool SecurityCheck()
    {
        if (!GameConfig.Instance.isDebugMode)
            return false;

        if (!DebugPanel)
            ComponentCheck();

        //当行数超过100时清除日志
        if (this.lineCount > 100)
        {
            LogText.text = "";
            this.lineCount = 0;
        }

        this.lineCount++;

        return true;
    }

    /// <summary>
    /// 显示普通日志 白色
    /// </summary>
    /// <param name="message"></param>
    public void ShowLog(string message)
    {
        if (!this.SecurityCheck())
            return;

        LogText.text += "<color=#000000>" + message + "</color>" + "\n";
        VerticalScrollbar.value = 0;
    }

    /// <summary>
    /// 显示警告日志 红色
    /// </summary>
    /// <param name="message"></param>
    public void ShowErrorLog(string message)
    {
        if (!this.SecurityCheck())
            return;

        LogText.text += "<color=#ff0000>" + message + "</color>" + "\n";
        VerticalScrollbar.value = 0;
    }
}
