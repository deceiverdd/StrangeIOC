using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAInputManager : MonoBehaviour
{
    public static bool IsUpward { get; private set; }
    public static bool IsDownward { get; private set; }
    public static bool IsLeftward { get; private set; }
    public static bool IsRightward { get; private set; }

    //private void OnEnable()
    //{
    //    InputListener.EventKeyboardInput += HandleKeyboardInput;
    //}

    //private void OnDisable()
    //{
    //    InputListener.EventKeyboardInput -= HandleKeyboardInput;
    //}

    private void Update()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            IsUpward = true;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            IsDownward = true;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            IsLeftward = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            IsRightward = true;
        }
        else
        {
            IsDownward = false;
            IsLeftward = false;
            IsRightward = false;
            IsUpward = false;
        }
    }

    private void HandleKeyboardInput(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                IsDownward = false;
                IsLeftward = false;
                IsRightward = false;
                IsUpward = true;
                break;
            case KeyCode.S:
                IsDownward = true;
                IsLeftward = false;
                IsRightward = false;
                IsUpward = false;
                break;
            case KeyCode.A:
                IsDownward = false;
                IsLeftward = true;
                IsRightward = false;
                IsUpward = false;
                break;
            case KeyCode.D:
                IsDownward = false;
                IsLeftward = false;
                IsRightward = true;
                IsUpward = false;
                break;
            default:
                break;
        }
    }
}
