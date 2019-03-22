using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入状态更新类
/// </summary>
public class Input_StateChange : MonoBehaviour
{
    /// <summary>
    /// 单例字段
    /// </summary>
    public static readonly Input_StateChange Instance;
    static Input_StateChange()
    {
        Debug.Log("输入状态更新开始");
    }
    /// <summary>
    /// 玩家管理器字段
    /// </summary>
    PlayerManager playerManager;
    /// <summary>
    /// 输入状态字段
    /// </summary>
    Input_State inputState;
    /// <summary>
    /// 按键设置字段
    /// </summary>
    KeyMapping[] inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;
        inputSetting = Input_Setting.DefaultKeySetting;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in PlayerManager.GetPlayerList())
        {
            ChangeInputState(inputSetting, PlayerManager.GetInputState(i.Key).GameOperations);
        }
    }
    /// <summary>
    /// 更新输入状态
    /// </summary>
    /// <param name="inputSetting"></param>
    /// <param name="gameOperation"></param>
    public void ChangeInputState(KeyMapping[] inputSetting, GameOperation[] gameOperation)
    {
        List<GameOperation> tempList = new List<GameOperation>();
        
        for (int i = 0; i < inputSetting.Length; i++)
        {
            //Debug.Log(inputSetting[i].keyPressType);
            switch (inputSetting[i].keyPressType)
            {
                
                //按下
                case KeyPressType.Down:
                    //Debug.Log(inputSetting[i].operation);
                    if (Input.GetKeyDown(inputSetting[i].keyLink))
                    {
                        Debug.Log(inputSetting[i].operation);
                        tempList.Add(inputSetting[i].operation);
                    }
                    break;
                //按住
                case KeyPressType.Hold:
                    if (Input.GetKey(inputSetting[i].keyLink))
                    {
                        Debug.Log(inputSetting[i].operation);
                        tempList.Add(inputSetting[i].operation);
                    }
                    break;
                //松开
                case KeyPressType.Up:
                    if (Input.GetKeyUp(inputSetting[i].keyLink))
                    {
                        Debug.Log(inputSetting[i].operation);
                        tempList.Add(inputSetting[i].operation);
                    }
                    break;
            }
            gameOperation = tempList.ToArray();
        }
    }
}
