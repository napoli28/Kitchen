using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Input_State
{
    public GUID PlayerID;
    public KeyMapping[] RuntimeInputSetting;
    public List<GameOperation> GameOperationList; 

    public Input_State(GUID playerID)
    {
        PlayerID = playerID;
        GameOperationList = new List<GameOperation>();
    }
}
