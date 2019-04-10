using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAPlayerCtr : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;

    [Inject]
    public PlayerModel playerModel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCtr();

        AnimationCtr();
    }

    /// <summary>
    /// 动画控制
    /// </summary>
    private void AnimationCtr()
    {
        if(DAInputManager.IsUpward)
        {
            _animator.SetBool("IsUpward", true);
        }
        else
        {
            _animator.SetBool("IsUpward", false);
        }

        if (DAInputManager.IsDownward)
        {
            _animator.SetBool("IsDownward", true);
        }
        else
        {
            _animator.SetBool("IsDownward", false);
        }

        if (DAInputManager.IsLeftward)
        {
            _animator.SetBool("IsLeftward", true);
        }
        else
        {
            _animator.SetBool("IsLeftward", false);
        }

        if (DAInputManager.IsRightward)
        {
            _animator.SetBool("IsRightward", true);
        }
        else
        {
            _animator.SetBool("IsRightward", false);
        }
    }

    /// <summary>
    /// 移动控制
    /// </summary>
    private void MoveCtr()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _transform.Translate(new Vector3(h, v, 0) * Time.deltaTime);
    }
}
