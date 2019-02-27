using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<NPCController>
{
    private static Patrol _instance;

    private Patrol()
    {
        if(_instance != null)
            return;

        _instance = this;
    }

    public static Patrol Instance
    {
        get
        {
            if (_instance == null)
                new Patrol();
            return _instance;
        }
    }

    public override void EnterState(NPCController owner)
    {
        
    }

    public override void ExitState(NPCController owner)
    {
        
    }

    public override void Update(NPCController owner)
    {
        //owner.StateMachine.ChangeState(Patrol.Instance);
    }
}
