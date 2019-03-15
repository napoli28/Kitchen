//using UnityEngine;
//using System.Collections;

//public class MainTest : MonoBehaviour
//{

//    StateMachine<Main> m_pStateMachine;//定义一个状态机

//    void Start()
//    {

//        m_pStateMachine = new StateMachine<Main> (this);//初始化状态机
//        m_pStateMachine.SetCurrentState(MainState_Ready.Instance()); //设置一个当前状态
//        m_pStateMachine.SetGlobalStateState(MainState.Instance());//设置全局状态
//    }

//    void Update()
//    {
//        m_pStateMachine.SMUpdate();
//    }

//    /*返回状态机*/
//    public StateMachine<Main> GetFSM()
//     {
//         return m_pStateMachine;
//     }   
//}