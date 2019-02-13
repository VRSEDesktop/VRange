using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State<AI>
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

    public override void EnterState(AI owner)
    {
        
    }

    public override void ExitState(AI owner)
    {
        
    }

    public override void Update(AI owner)
    {
        //owner.StateMachine.ChangeState(Patrol.Instance);
    }
}
