using Need.Mx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState_Walking : CharacterBehaviour, ICharacterState
{
    public CharacterState_Walking(Character character)
    {
        this.character = character;
        EventController.AddEventListener(character.PlayerID.ToString() + "_Input_" + "Move", Move);
        Debug.Log("------------------------character in StandingState~!（进入移动状态！）");
    }
    public void HandleInput()
    {


        //Idle
        if ((Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") == 0))
        {
            character.SetCharacterState(new CharacterState_Idle(character));
            return;
        }

        //Jump
        if (Input.GetButton("Jump"))
        {
            character.SetCharacterState(new JumpingState(character));
            return;
        }


    }

    public void EnterExecute()
    {

    }

    public void UpdateExecute()
    {
        character.action.Walking();
    }

    public void ExitExecute()
    {
        EventController.RemoveEventListener();
    }
}
