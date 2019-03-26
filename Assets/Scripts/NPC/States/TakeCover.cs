using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

class TakeCover : State<AIController>
{
    private static TakeCover _instance;

    public TakeCover()
    {
        if (_instance != null)
            return;

        _instance = this;
    }

    public static TakeCover Instance
    {
        get
        {
            if (_instance == null)
                new TakeCover();
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
