using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    [SerializeField]
    private Button BtnClose;
    [SerializeField]
    private Button BtnSetting;
    [SerializeField]
    private Button BtnPause;

    public override void Awake()
    {
        RegisterBtn();

        base.Awake();
    }

    /// <summary>
    /// 注册按钮
    /// </summary>
    private void RegisterBtn()
    {
        BtnClose = FindBtn("BtnClose");
        BtnPause = FindBtn("BtnPause");
        BtnSetting = FindBtn("BtnSetting");

        EventTriggerListener.Get(BtnClose.gameObject).onClick = OnButtonClick;
        EventTriggerListener.Get(BtnPause.gameObject).onClick = OnButtonClick;
        EventTriggerListener.Get(BtnSetting.gameObject).onClick = OnButtonClick;
    }

    private void OnButtonClick(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == BtnClose.gameObject)
        {
            Debuger.Log("BtnClose");
        }
        if (go == BtnSetting.gameObject)
        {
            Debuger.Log("BtnSetting");
        }
        if (go == BtnPause.gameObject)
        {
            Debuger.Log("BtnPause");
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
