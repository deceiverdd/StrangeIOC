using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class UIManager : Singleton<UIManager>
{
    private readonly string panelPrefabPath = Application.dataPath + @"/Resources/UIPanelPrefab";
    private readonly string jsonPath = Application.dataPath + @"/Json/UIJson.json";
    private List<UIPanel> PanelList;
    private Dictionary<UIPanelType, BasePanel> PanelDic;
    private Transform canvasTransform;
    public Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
                canvasTransform = GameObject.Find("Canvas").transform;

            return canvasTransform;
        }
    }
    private Stack<BasePanel> CurPanelStack;

    public UIManager()
    {
        UIPanelInfoSaveInJson();
    }

    public void UIPanelInfoSaveInJson()
    {
        PanelList = JsonIO.LoadJsonFromFile<List<UIPanel>>(panelPrefabPath);

        DirectoryInfo folder = new DirectoryInfo(panelPrefabPath);

        foreach (var file in folder.GetFiles("*.prefab"))
        {
            UIPanelType type = (UIPanelType)Enum.Parse(typeof(UIPanelType), file.Name.Replace(".prefab", ""));

            string path = @"UIPanelPrefab/" + file.Name.Replace(".prefab", "");

            bool UIPanelExistInList = false;

            UIPanel uiPanel = PanelList.SearchPanelForType(type);

            if(uiPanel != null)
            {
                uiPanel.UIPanelPrefabPath = path;
                UIPanelExistInList = true;
            }

            if(!UIPanelExistInList)
            {
                UIPanel panel = new UIPanel
                {
                    UIPanelPrefabPath = path,
                    UIPanelType = type
                };

                PanelList.Add(panel);
            }
        }
    }

    public BasePanel GetPanel(UIPanelType type)
    {
        if (PanelDic == null)
        {
            PanelDic = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel = PanelDic.TryGetValue(type);

        if(panel == null)
        {
            string path = PanelList.SearchPanelForType(type).UIPanelPrefabPath;

            if(path == null)
                throw new Exception("找不到该UIPanelType的Prefab");

            if (Resources.Load(path) == null)
                throw new Exception("找不到该path的prefab");

            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;

            instPanel.transform.SetParent(CanvasTransform, false);

            PanelDic.Add(type, instPanel.GetComponent<BasePanel>());

            return instPanel.GetComponent<BasePanel>();
        }

        return panel;
    }

    public void PushPanel(UIPanelType type)
    {
        if (CurPanelStack == null)
            CurPanelStack = new Stack<BasePanel>();

        if(CurPanelStack.Count > 0)
        {
            BasePanel topPanel = CurPanelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(type);
        CurPanelStack.Push(panel);
        panel.OnEnter();
    }

    public void PopPanel()
    {
        if (CurPanelStack == null)
            CurPanelStack = new Stack<BasePanel>();

        if (CurPanelStack.Count <= 0)
            return;

        BasePanel topPanel = CurPanelStack.Pop();
        topPanel.OnExit();

        if (CurPanelStack.Count <= 0)
            return;

        BasePanel nextTopPanel = CurPanelStack.Peek();
        nextTopPanel.OnResume();
    }
}

public class UIPanel
{
    public UIPanelType UIPanelType;
    public string UIPanelPrefabPath;
}

public enum UIPanelType
{
    MainMenuPanel,
    SystemSettingPanel,
    PausePanel,
    StorePanel,
}


