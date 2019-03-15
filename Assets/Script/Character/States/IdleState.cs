using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    private Character _character;

    public IdleState(Character character)
    {
        _character = character;
        Debug.Log("------------------------character in StandingState~!（进入站立状态！）");
    }

    public void HandleInput()
    {
        //Walking
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            _character.SetCharacterState(new WalkingState(_character));
            return;
        }
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            _character.SetCharacterState(new JumpingState(_character));
            return;
        }

       
       
        //if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        //{
        //    Debug.Log("get KeyCode.DownArrow!");
        //    _character.SetcharacterState(new DuckingState(_character));
        //}
    }
    public void EnterExecute()
    {
        _character._action.Idle();
    }

    public void UpdateExecute()
    {
       
    }

    public void ExitExecute()
    {

    }
}
