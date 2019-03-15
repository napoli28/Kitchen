using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    Character _character;
    //将名称转换为哈希值可以提高索引的速度
    private int _moveZ = Animator.StringToHash("moveZ");
    private int _jump = Animator.StringToHash("jump");
    private int _land = Animator.StringToHash("land");
    private int _move = Animator.StringToHash("move");
    private int _moveX = Animator.StringToHash("moveX");
    private int _run = Animator.StringToHash("run");
    public float _speed = 1;
    private Animator _animator;

    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }

    public Action(Character character)
    {
        _character = character;
        _animator = _character._animator;
    }
    float _vertical;
    float _horizontal;
    //float running;
    //if (Input.GetButton("Run"))
    //{
    //    speed = Mathf.Lerp(speed, 2f, 0.2f);
    //}
    //else
    //{
    //    speed = Mathf.Lerp(speed, 1f, 0.2f);
    //}


    //移动
    //Vector3 forward = transform.forward;
    

    //transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
    //播放动画
    
    //

    public void Jump()
    {
        Debug.Log("Jumping!");
        _animator.SetBool(_jump, true);
        _character.transform.position += new Vector3(0f, 5f, 0f);
    }

    public void Land()
    {
        Debug.Log("Landing!");
        _animator.SetBool(_land, true);
    }

    public void Walking()
    {
        _animator.SetBool(_move, true);
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _animator.SetFloat(_moveZ, _vertical);
        _animator.SetFloat(_moveX, _horizontal);
        _character.transform.position += Vector3.Normalize(_character.transform.forward * _vertical + _character.transform.right * _horizontal) * Time.deltaTime * 3f * _speed;
    }
    public void Idle()
    {
        Debug.Log("Idle!");
        _animator.SetBool(_move, false);
    }
}
