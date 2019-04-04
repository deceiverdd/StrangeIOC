using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAsyncCtr : MonoBehaviour
{
    [Tooltip("加载进度条")]
    public Slider ProgressBar;
    [Tooltip("加载进度")]
    public Text ProgressText;

    private static AsyncOperation mAsyncOperation;
    private int mCurProgress = 0;
    private bool startLoadLevelAsyncCheck = false;

    private void Start()
    {
        this.StartLoadLevelAsync(GameConfig.Instance.curLevel + 2);
    }

    private void Update()
    {
        LoadLevelAsyncCheck();
    }

    void InitComponent()
    {
        startLoadLevelAsyncCheck = false;
    }

    private void LoadLevelAsyncCheck()
    {
        if (mAsyncOperation == null || !startLoadLevelAsyncCheck)
            return;

        int progressBar = 0;

        if (mAsyncOperation.progress < 0.8)
            progressBar = (int)(mAsyncOperation.progress * 100);
        else
            progressBar = 100;

        if (mCurProgress < progressBar)
        {
            mCurProgress++;
            //进度条ui显示
            ProgressBar.value = mCurProgress * 0.01f;
            ProgressText.text = mCurProgress.ToString() + "%";
        }
        else
        {
            // 必须等进度条跑到100%才允许切换到下一场景
            if (progressBar == 100) mAsyncOperation.allowSceneActivation = true;
        }
    }

    private void StartLoadLevelAsync(string levelName)
    {
        startLoadLevelAsyncCheck = true;
        StartCoroutine(CoroutineLoadScene(levelName));
    }

    private void StartLoadLevelAsync(int index)
    {
        startLoadLevelAsyncCheck = true;
        StartCoroutine(CoroutineLoadScene(index));
    }

    private IEnumerator CoroutineLoadScene(string levelName)
    {
        yield return null;

        // u3d 5.3之后使用using UnityEngine.SceneManagement;加载场景
        mAsyncOperation = SceneManager.LoadSceneAsync(levelName);
        // 不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
        mAsyncOperation.allowSceneActivation = false;
        // mAsyncOperation.progress测试只有0和0.9(其实只有固定的0.89...)
        // 所以大概大于0.8就当是加载完成了
        while (!mAsyncOperation.isDone && mAsyncOperation.progress < 0.8f)
        {
            yield return mAsyncOperation;
        }
    }

    private IEnumerator CoroutineLoadScene(int index)
    {
        yield return null;

        // u3d 5.3之后使用using UnityEngine.SceneManagement;加载场景
        mAsyncOperation = SceneManager.LoadSceneAsync(index);
        // 不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
        mAsyncOperation.allowSceneActivation = false;
        // mAsyncOperation.progress测试只有0和0.9(其实只有固定的0.89...)
        // 所以大概大于0.8就当是加载完成了
        while (!mAsyncOperation.isDone && mAsyncOperation.progress < 0.8f)
        {
            yield return mAsyncOperation;
        }
    }
}
