using UnityEngine;

[System.Serializable]
public class StateMachine<T>
{
    public State<T> CurrentState { get; private set; }
    public T Owner;

    public StateMachine(T owner)
    {
        Owner = owner;
        CurrentState = null;
    }
       
    public void ChangeState(State<T> newState)
    {
        if(CurrentState != null)
            CurrentState.ExitState(Owner);
        CurrentState = newState;
        CurrentState.EnterState(Owner);
    }

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update(Owner);
        }
    }

    public void OnTriggerStay(T owner, Collider other)
    {
        if (CurrentState != null)
        {
            CurrentState.OnTriggerStay(owner, other);
        }
    }

    public void OnTriggerExit(T owner, Collider other)
    {
        if (CurrentState != null)
        {
            CurrentState.OnTriggerExit(owner, other);
        }
    }

    public override string ToString()
    {
        return CurrentState.GetType().Name;
    }
}

public abstract class State<T>
{
    public abstract void EnterState(T owner);
    public abstract void ExitState(T owner);
    public abstract void Update(T owner);
    public abstract void OnTriggerStay(T owner, Collider other);
    public abstract void OnTriggerExit(T owner, Collider other);
}
