using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour {

    public bool isNeedDestroy;
    public bool isRandomMove;
    public bool isMoveForward;
    public float timeToDestroy = 3f;
    public float moveSpeed = 1f;

    private float timeCount;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        TimeCount();

        AutoDestroyCheck();

        RandomMove();

        MoveForward();
	}

    void TimeCount()
    {
        timeCount += Time.deltaTime;
    }

    void AutoDestroyCheck()
    {
        if (!isNeedDestroy)
            return;

        if (timeCount >= timeToDestroy)
            this.gameObject.SetActive(false);
    }

    void RandomMove()
    {
        if (!isRandomMove)
            return;

        System.Random random = new System.Random();//声明一个随机数类
        float direction = (float)random.Next(0, 360);//在0--360之间随机生成一个单精度小数)
        transform.rotation = Quaternion.Euler(0, direction, 0);//旋转指定度数
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//随机方向移动
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
