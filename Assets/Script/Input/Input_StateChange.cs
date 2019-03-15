using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_StateChange : MonoBehaviour
{
    public static readonly Input_StateChange Instance;
    static Input_StateChange()
    {
        Debug.Log("玩家输入状态实例化");
    }
    
    PlayerManager playerManager;
    Input_State inputState;
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
            ChangeInputState(inputSetting, PlayerManager.GetOperations(i.Key));
        }
    }
    /// <summary>
    /// 更新输入状态
    /// </summary>
    /// <param name="inputSetting"></param>
    /// <param name="gameOperation"></param>
    public void ChangeInputState(KeyMapping[] inputSetting, List<GameOperation> gameOperation)
    {
        gameOperation.Clear();
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
                        gameOperation.Add(inputSetting[i].operation);
                    }
                    break;
                //按住
                case KeyPressType.Hold:
                    if (Input.GetKey(inputSetting[i].keyLink))
                    {
                        Debug.Log(inputSetting[i].operation);
                        gameOperation.Add(inputSetting[i].operation);
                    }
                    break;
                //松开
                case KeyPressType.Up:
                    if (Input.GetKeyUp(inputSetting[i].keyLink))
                    {
                        Debug.Log(inputSetting[i].operation);
                        gameOperation.Add(inputSetting[i].operation);
                    }
                    break;
            }
        }
    }
}
