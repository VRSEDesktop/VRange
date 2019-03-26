﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

class Surrender : State<AIController>
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

    public override void EnterState(AIController owner)
    {
        //Set anims to surrender anims

        //Stop the navmesh agent
        owner.Agent.isStopped = true;
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
