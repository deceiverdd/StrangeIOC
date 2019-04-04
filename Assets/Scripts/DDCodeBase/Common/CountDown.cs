using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private int totalNum;
    private GameObject[] numImageObj;
    private float count = 0f;
    private int curNum = 0;

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Count();
    }

    private void Init()
    {
        curNum = 0;
        totalNum = transform.childCount;
        numImageObj = new GameObject[totalNum];

        for (int i = 0; i < totalNum; i++)
        {
            numImageObj[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < totalNum; i++)
        {
            if (i == totalNum - 1)
            {
                numImageObj[i].SetActive(true);
            }
            else
            {
                numImageObj[i].SetActive(false);
            }
        }
    }

    private void Count()
    {
        if (curNum > totalNum)
        {
            //Debuger.Log(totalNum + "秒倒计时结束");
            return;
        }

        count += Time.deltaTime;

        if (count >= 1)
        {
            curNum++;
            ChangeNum();
            count = 0f;
        }
    }

    private void ChangeNum()
    {
        for (int i = 0; i < totalNum; i++)
        {
            if(i == totalNum - 1 - curNum)
            {
                numImageObj[i].SetActive(true);
            }
            else
            {
                numImageObj[i].SetActive(false);
            }
        }
    }

}
