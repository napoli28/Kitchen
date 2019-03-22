using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    /// <summary>
    /// 玩家唯一标识
    /// </summary>
    public GUID PlayerID;
    /// <summary>
    /// 角色实例字段
    /// </summary>
    Character character;
    /// <summary>
    /// 角色状态组
    /// </summary>
    ICharacterState[] states;
    /// <summary>
    /// 角色当前状态
    /// </summary>
    ICharacterState currentState;

    public Action action;
    public Animator animator;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        currentState.HandleInput();
        currentState.UpdateExecute();
    }

    void OnCollisionEnter(Collision _collision)
    {
        if (currentState.GetType().Name == "JumpingState")
        {
            Debug.Log(currentState.GetType().Name);
            if (_collision.gameObject.tag == "ground")
            {
                Debug.Log(_collision.gameObject.tag);
                character.SetCharacterState(new CharacterState_Idle(character));
            }
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Initialize()
    {
        character = this;
        states = new ICharacterState[]{
            new CharacterState_Idle(character),
            new CharacterState_Walking(character),
            new CharacterState_Empty(character),
            new CharacterState_Holding(character)
            };
        currentState = states[0];
        animator = character.GetComponent<Animator>();
        Debug.Log(animator);
        action = new Action(character);
    }
    /// <summary>
    /// 更换状态
    /// </summary>
    /// <param name="state"></param>
    public void SetCharacterState(ICharacterState state)
    {
        currentState = state;
    }


}