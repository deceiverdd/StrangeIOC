using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoSingleton<MessageBox>
{
    public Text messageText;
    public GameObject messageBox;

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitComponent()
    {
        this.messageBox.SetActive(false);
    }

    public void ShowMessageBox(string msg,float durationTime = 2f)
    {
        this.messageText.text = msg;
        this.messageBox.SetActive(true);
        StartCoroutine(CorRecoverMsgBox(durationTime));
    }

    IEnumerator CorRecoverMsgBox(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);

        this.messageBox.SetActive(false);
    }

    private void OnEnable()
    {
        InitComponent();
    }
}
