using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameOperation
{
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
    PickUp,
    PutDown,
    Use,
    Throw,
    Dash
}

public enum KeyPressType
{
    Down,
    Hold,
    Up
}
public struct KeyMapping
{
    public GameOperation operation;
    public KeyCode keyLink;
    public KeyPressType keyPressType;
    int trigger;
}
/// <summary>
/// 按键设置类
/// </summary>
public static class Input_Setting
{
    /// <summary>
    /// 默认按键设置
    /// </summary>
    public static KeyMapping[] DefaultKeySetting =
    {
        new KeyMapping
        {
            operation = GameOperation.MoveLeft,
            keyLink = KeyCode.A,
            keyPressType = KeyPressType.Hold
        },
        new KeyMapping
        {
            operation = GameOperation.MoveRight,
            keyLink = KeyCode.D,
            keyPressType = KeyPressType.Hold
        },
        new KeyMapping
        {
            operation = GameOperation.MoveUp,
            keyLink = KeyCode.W,
            keyPressType = KeyPressType.Hold
        },
        new KeyMapping
        {
            operation = GameOperation.MoveDown,
            keyLink = KeyCode.S,
            keyPressType = KeyPressType.Hold
        },
        new KeyMapping
        {
            operation = GameOperation.PickUp,
            keyLink = KeyCode.Space,
            keyPressType = KeyPressType.Down
        },
        new KeyMapping
        {
            operation = GameOperation.PutDown,
            keyLink = KeyCode.Space,
            keyPressType = KeyPressType.Down
        },
        new KeyMapping
        {
            operation = GameOperation.Throw,
            keyLink = KeyCode.LeftControl,
            keyPressType = KeyPressType.Down
        },
        new KeyMapping
        {
            operation = GameOperation.Use,
            keyLink = KeyCode.X,
            keyPressType = KeyPressType.Down
        },
    };

    public static KeyMapping[] CustomInputSetting;
}
