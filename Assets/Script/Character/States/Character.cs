using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    private int _airJumpCount = 0;

    public int AirJumpCount
    {
        get { return _airJumpCount; }
        set { _airJumpCount = value; }
    }

    Character _character;
    CharacterState _state;
    public Action _action;
    public Animator _animator;

    public Character()
    {
        _character = this;
        _state = new IdleState(_character);
        
    }

    public void SetCharacterState(CharacterState newState)
    {
        _state.ExitExecute();
        _state = newState;
        _state.EnterExecute();
    }

    void Start()
    {
        _animator = _character.GetComponent<Animator>();
        Debug.Log(_animator);
        _action = new Action(_character);
        
    }

    void Update()
    {
        _state.HandleInput();
        _state.UpdateExecute();
    }

    void OnCollisionEnter(Collision _collision)
    {
        if (_state.GetType().Name == "JumpingState")
        {
            Debug.Log(_state.GetType().Name);
            if (_collision.gameObject.tag == "ground")
            {
                Debug.Log(_collision.gameObject.tag);
                _character.SetCharacterState(new IdleState(_character));
            }
        }
    }
}