using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

class ActSuspicious : State<AIController>
{
    private static ActSuspicious _instance;

    public ActSuspicious()
    {
        if (_instance != null)
            return;

        _instance = this;
    }

    public static ActSuspicious Instance
    {
        get
        {
            if (_instance == null)
                new ActSuspicious();
            return _instance;
        }
    }

    public override void EnterState(AIController owner)
    {

    }

    public override void ExitState(AIController owner)
    {

    }

    public override void OnTriggerExit(AIController owner, Collider other)
    {

    }

    public override void OnTriggerStay(AIController owner, Collider other)
    {

    }

    public override void Update(AIController owner)
    {

    }
}
