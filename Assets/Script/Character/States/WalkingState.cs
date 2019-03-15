using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : CharacterState
{
    private Character _character;

    public WalkingState(Character character)
    {
        _character = character;

        Debug.Log("------------------------character in StandingState~!（进入移动状态！）");
    }
    public void HandleInput()
    {
        //Idle
        if ((Input.GetAxis("Vertical") == 0) &&(Input.GetAxis("Horizontal") == 0))
        {
            _character.SetCharacterState(new IdleState(_character));
            return;
        }

        //Jump
        if (Input.GetButton("Jump"))
        {
            _character.SetCharacterState(new JumpingState(_character));
            return;
        }


    }

    public void EnterExecute()
    {

    }

    public void UpdateExecute()
    {
       _character._action.Walking();
    }

    public void ExitExecute()
    {

    }
}
