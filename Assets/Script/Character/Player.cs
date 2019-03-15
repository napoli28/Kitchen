using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public struct Player
{
    public GUID PlayerID;
    public Input_State inputState;
    public Character Character;

    public Player(GUID playerID)
    {
        PlayerID = playerID;
        inputState = new Input_State(playerID);
        Debug.Log("玩家输入状态实例化");
        Character = Resources.Load<GameObject>("Character").GetComponent<Character>();
        Character.PlayerID = playerID;
        Debug.Log("玩家控制角色实例化");
    }
}

