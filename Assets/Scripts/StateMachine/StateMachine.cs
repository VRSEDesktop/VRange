using UnityEngine;

namespace Assets.Scripts.StateMachine
{
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
            if (CurrentState != null)
                CurrentState.ExitState(Owner);

            CurrentState = newState;
            CurrentState.EnterState(Owner);
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.Update(Owner);
        }

        public void OnTriggerStay(Collider other)
        {
            if (CurrentState != null)
                CurrentState.OnTriggerStay(Owner, other);
        }

        public void OnTriggerExit(Collider other)
        {
            if (CurrentState != null)
                CurrentState.OnTriggerExit(Owner, other);
        }

        /// <summary>
        /// Returns the name of the current state.
        /// </summary>
        public override string ToString()
        {
            return CurrentState.GetType().Name;
        }
    }

}