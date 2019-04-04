using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneCtr : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        InputListener.EventKeyboardInput += ReceiveKeyboardInput;
    }

    protected virtual void OnDisable()
    {
        InputListener.EventKeyboardInput -= ReceiveKeyboardInput;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        SceneInitialize();
    }

    protected virtual void SceneInitialize()
    {
        GameStateMgr.ChangeGameState(GameStateMgr.GameState.Gaming);
    }

    protected abstract void ReceiveKeyboardInput(KeyCode key);
}
