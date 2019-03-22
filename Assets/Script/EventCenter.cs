
namespace Need.Mx
{
    using System;

    [Serializable]
    public class EventException : Exception
    {
        public EventException(string message) : base(message)
        {
        }

        public EventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

namespace Need.Mx
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// 由EventDispatcher持有，请不用直接使用此类。
    /// </summary>
    public static class EventController
    {
        //永久性的消息，在Cleanup的时候，这些消息的响应是不会被清理的。
        private static List<string> m_permanentEvents = new List<string>();
        private static Dictionary<string, Delegate> m_theRouter = new Dictionary<string, Delegate>();

        public static void AddEventListener(string eventType, Action handler)
        {
            OnListenerAdding(eventType, handler);
            m_theRouter[eventType] = (Action)Delegate.Combine((Action)m_theRouter[eventType], handler);
        }

        public static void AddEventListener<T>(string eventType, Action<T> handler)
        {
            OnListenerAdding(eventType, handler);
            m_theRouter[eventType] = (Action<T>)Delegate.Combine((Action<T>)m_theRouter[eventType], handler);
        }

        public static void Cleanup()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Delegate> pair in m_theRouter)
            {
                bool flag = false;
                foreach (string str in m_permanentEvents)
                {
                    if (pair.Key == str)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Add(pair.Key);
                }
            }
            foreach (string str in list)
            {
                m_theRouter.Remove(str);
            }
        }
        public static bool ContainsEvent(string eventType)
        {
            return m_theRouter.ContainsKey(eventType);
        }
        public static void MarkAsPermanent(string eventType)
        {
            m_permanentEvents.Add(eventType);
        }


        /// <summary>
        /// 事件添加
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listenerBeingAdded"></param>
        private static void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
        {
            if (!m_theRouter.ContainsKey(eventType))
            {
                m_theRouter.Add(eventType, null);
            }
            Delegate delegate2 = m_theRouter[eventType];
            if ((delegate2 != null) && (delegate2.GetType() != listenerBeingAdded.GetType()))
            {
                throw new EventException(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, delegate2.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        private static void OnListenerRemoved(string eventType)
        {
            if (m_theRouter.ContainsKey(eventType) && (m_theRouter[eventType] == null))
            {
                m_theRouter.Remove(eventType);
            }
        }

        private static bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            if (!m_theRouter.ContainsKey(eventType))
            {
                return false;
            }
            Delegate delegate2 = m_theRouter[eventType];
            if ((delegate2 != null) && (delegate2.GetType() != listenerBeingRemoved.GetType()))
            {
                throw new EventException(string.Format("Remove listener {0}\" failed, Current type is {1}, adding type is {2}.", eventType, delegate2.GetType(), listenerBeingRemoved.GetType()));
            }
            return true;
        }


        public static void RemoveEventListener(string eventType, Action handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                m_theRouter[eventType] = (Action)Delegate.Remove((Action)m_theRouter[eventType], handler);
                OnListenerRemoved(eventType);
            }
        }
        public static void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                m_theRouter[eventType] = (Action<T>)Delegate.Remove((Action<T>)m_theRouter[eventType], handler);
                OnListenerRemoved(eventType);
            }
        }

        public static void TriggerEvent(string eventType)
        {
            Delegate delegate2;
            if (m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action action = invocationList[i] as Action;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action();
                    }
                    catch (Exception exception)
                    {
                        string.Format(exception.Message);
                    }
                }
            }
        }
        public static void TriggerEvent<T>(string eventType, T arg1)
        {
            Delegate delegate2;
            if (m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T> action = invocationList[i] as Action<T>;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1);
                    }
                    catch (Exception exception)
                    {
                        // Log output
                        string.Format(exception.Message);
                    }
                }
            }
        }

        public static Dictionary<string, Delegate> TheRouter
        {
            get
            {
                return m_theRouter;
            }
        }
    }
}

