using UnityEngine;
public class State<T>
{
    public T Target;
    //Enter state  
    public virtual void Enter(T entityType)
    {

    }
    //Execute state
    public virtual void Execute(T entityType)
    {

    }
    //Exit state
    public virtual void Exit(T entityType)
    {

    }

    //FixedUpDate
    public virtual void FixedExecute(T entityType)
    {

    }
}
