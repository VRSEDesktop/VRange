using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

class TakeCover : State<NPCController>
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

    public override void EnterState(NPCController owner)
    {

    }

    public override void ExitState(NPCController owner)
    {

    }

    public override void OnTriggerExit(NPCController owner, Collider other)
    {

    }

    public override void OnTriggerStay(NPCController owner, Collider other)
    {

    }

    public override void Update(NPCController owner)
    {

    }
}
