using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : CharacterState
{ 
    private Character _character;
    private int _airJumpCount;
    public JumpingState(Character character)
    {
        _character = character;
        _airJumpCount = _character.AirJumpCount;

        Debug.Log("------------------------Heroine in JumpingState~!(进入跳跃状态！)");
    }

    public void HandleInput()
    {
        if (Input.GetButtonDown("Jump") && _airJumpCount > 0)
        {
            _character._action.Jump();
            _airJumpCount -= 1;
        }
    }

    public void EnterExecute()
    {
        _character._action.Jump();
    }

    public void UpdateExecute()
    {

    }
    public void ExitExecute()
    {
        _character._action.Land();
    }
}
