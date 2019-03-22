using Need.Mx;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 该类型用于发布操作事件
/// </summary>
public class Input_Event
{
    /// <summary>
    /// 玩家唯一标识
    /// </summary>
    private GUID playerID;
    /// <summary>
    /// 玩家ID转字符串
    /// </summary>
    private string strPlayerID;
    /// <summary>
    /// 当前帧输入操作
    /// </summary>
    private GameOperation[] operations;
    /// <summary>
    /// 构造函数，初始化
    /// </summary>
    /// <param name="playerID"></param>
    public Input_Event(GUID playerID)
    {
        this.playerID = playerID;
        strPlayerID = playerID.ToString();
        operations = PlayerManager.GetInputState(playerID).GameOperations;
    }

    /// <summary>
    /// 事件名数组
    /// </summary>
    private List<string> events = new List<string>();
    /// <summary>
    /// 遍历当前帧操作并发布事件
    /// </summary>
    public void PublicEvent()
    {
        if (operations.Length == 0)
        {
            events.Add(playerID.ToString() + "_Input_" + "AFK");
        }
        else
        {
            foreach (var item in operations)
            {
                if (item == GameOperation.MoveDown || item == GameOperation.MoveUp || item == GameOperation.MoveLeft || item == GameOperation.MoveRight)
                {
                    if (!events.Contains(strPlayerID + "_Input_" + "Move"))
                    {
                        events.Add(strPlayerID + "_Input_" + "Move");
                    }
                }
                else
                {
                    events.Add(strPlayerID + "_Input_" + item.ToString());
                }
            }
        }
        foreach (var item in events)
        {
            EventController.TriggerEvent(item);
        }
        events.Clear();
    }
}

