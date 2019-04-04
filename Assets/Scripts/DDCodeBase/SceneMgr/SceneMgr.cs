using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    public static SceneName CurScene { get; private set; } = SceneName.None;
    public static SceneName NextAsyncLoadScene { get; private set; } = SceneName.None;

    /// <summary>
    /// 用于加载较大场景，会进入一个临时的LoadScene场景用于异步加载后面的大场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadLevelAsync(SceneName sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName.ToString()) == null)
        {
            ToolKit.UIErrorDebug(sceneName.ToString() + "未添加到SceneBuild");
            return;
        }
        else
        {
            ToolKit.UIDebug("加载" + SceneManager.GetSceneByName(sceneName.ToString()).name);
        }

        NextAsyncLoadScene = sceneName;
        SceneManager.LoadScene(SceneName.LoadScene.ToString());
    }

    /// <summary>
    /// 立即加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(SceneName sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName.ToString()) == null)
        {
            DebugLogInUI.Instance.ShowErrorLog(sceneName.ToString() + "未添加到SceneBuild");
            return;
        }

        SceneManager.LoadScene(sceneName.ToString());
    }

    /// <summary>
    /// 返回待机界面
    /// </summary>
    public void BackToStandbyScene()
    {
        SceneManager.LoadScene(SceneName.StandbyScene.ToString());
    }

    /// <summary>
    /// 改变当前关卡
    /// </summary>
    /// <param name="sceneName"></param>
    public static void ChangeCurScene(SceneName sceneName)
    {
        CurScene = sceneName;
    }
}

public enum SceneName
{
    None = -3,
    LoadScene,
    StandbyScene,
    Battle_0 = 0,
}

