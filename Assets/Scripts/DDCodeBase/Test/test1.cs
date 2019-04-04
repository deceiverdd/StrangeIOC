using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test1 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("test1Awake");
    }

    private void OnEnable()
    {
        Debug.Log("test1OnEnable");
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("test1Start");
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    private void OnDisable()
    {
        Debug.Log("test1OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("test1OnDestroy");
    }

    void InputCheck()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("test2");
        }
    }
}
