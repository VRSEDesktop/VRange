using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Weapons.Abstraction;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

class Surrender : State<NPCController>
{
    private static Surrender _instance;

    private Surrender()
    {
        if (_instance != null)
            return;

        _instance = this;
    }

    public static Surrender Instance
    {
        get
        {
            if (_instance == null)
                new Surrender();
            return _instance;
        }
    }

    public override void EnterState(NPCController owner)
    {
        //Set anims to surrender anims

        //Stop the navmesh agent
        owner.Agent.isStopped = true;
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
