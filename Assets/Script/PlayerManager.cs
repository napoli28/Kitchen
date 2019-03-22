using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager()
    {
        Debug.Log("玩家管理器实例化");
    }
    /// <summary>
    /// 单例
    /// </summary>
    public static readonly PlayerManager Instance;

    
    public static Dictionary<GUID, Player> PlayerList;
    // Start is called before the first frame update
    void Start()
    {
        GUID playerID = GUID.Generate();
        PlayerList = new Dictionary<GUID, Player>
        {
            { playerID, new Player(playerID) }
        };
        Debug.Log("玩家实例化 ID:" + playerID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Dictionary<GUID, Player> GetPlayerList()
    {
        return PlayerList;
    }

    public static Player GetPlayer(GUID playerID)
    {
        return PlayerList[playerID];
    }

    public static Input_State GetInputState(GUID playerID)
    {
        return PlayerList[playerID].inputState;
    }

    public static Input_Event GetInputEvent(GUID playerID)
    {
        return PlayerList[playerID].inputEvent;
    }
}
