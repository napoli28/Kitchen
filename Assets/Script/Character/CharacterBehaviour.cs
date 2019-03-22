using Need.Mx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour
{
    /// <summary>
    /// 角色字段
    /// </summary>
    protected Character character;
    /// <summary>
    /// 玩家ID转字符串
    /// </summary>
    protected string strPlayerID;
    /// <summary>
    /// 走路速度
    /// </summary>
    float walkSpeed;
    /// <summary>
    /// 检测移动输入
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="handler"></param>
    public void CheckMove(string eventType, System.Action handler)
    {
        EventController.AddEventListener(eventType, handler);
    }
    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        Vector3 direction = PlayerManager.GetInputState(character.PlayerID).Axis;
        direction.x = Mathf.Ceil(direction.x);
        direction.z = Mathf.Ceil(direction.y);
        direction.y = 0f;
        direction = Vector3.Normalize(direction);
        character.transform.position += direction * walkSpeed * Time.deltaTime;
    }

    public void FindItem()
    {

    }
    public void PickUp()
    {

    }
    public void StateChange()
    {
        
    }
}
