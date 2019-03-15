using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class State<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void Update(T owner);
        public abstract void OnTriggerStay(T owner, Collider other);
        public abstract void OnTriggerExit(T owner, Collider other);
    }
}
