using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test2 : MonoSingleton<test2>
{
    private void Awake()
    {
        Debug.Log("test2Awake");
    }

    private void OnEnable()
    {
        Debug.Log("test2OnEnable");
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("test2Start");
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    private void OnDisable()
    {
        Debug.Log("test2OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("test2OnDestroy");
    }

    void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("test1");
        }
    }
}
