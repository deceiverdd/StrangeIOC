using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        yield return new WaitForEndOfFrame();
        SceneMgr.Instance.LoadScene(SceneName.StandbyScene);
    }
}
