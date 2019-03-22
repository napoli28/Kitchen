using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Input_State
{
    public GUID PlayerID;
    public KeyMapping[] RuntimeInputSetting;
    public GameOperation[] GameOperations;
    public Vector2 Axis;

    public Input_State(GUID playerID)
    {
        PlayerID = playerID;
        GameOperations = new GameOperation[0];
    }
}
