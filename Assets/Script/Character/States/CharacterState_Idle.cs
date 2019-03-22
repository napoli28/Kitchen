using Need.Mx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState_Idle : CharacterBehaviour, ICharacterState
{
    /// <summary>
    /// 构造函数，初始化
    /// </summary>
    public CharacterState_Idle(Character character)
    {
        this.character = character;
        Debug.Log("------------------------character in StandingState~!（进入站立状态！）");
        EventController.AddEventListener(character.PlayerID.ToString() + "_Input_" + GameOperation.ToString();, Move);
    }
    public void HandleInput()
    {
        //Walking
        //CheckMove();
        ////Jump
        //if (Input.GetButtonDown("Jump"))
        //{
        //    _character.SetCharacterState(new JumpingState(_character));
        //    return;
        //}



        //if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        //{
        //    Debug.Log("get KeyCode.DownArrow!");
        //    _character.SetcharacterState(new DuckingState(_character));
        //}
    }
    public void EnterExecute()
    {
        character.action.Idle();
    }

    public void UpdateExecute()
    {
       
    }

    public void ExitExecute()
    {

    }

}
