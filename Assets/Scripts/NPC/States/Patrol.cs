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
        if (!owner.Agent.pathPending && owner.Agent.remainingDistance < 0.5f)
            GotoNextPoint(owner);
    }

    public override void OnTriggerStay(NPCController owner, Collider other)
    {
        
    }

    public override void OnTriggerExit(NPCController owner, Collider other)
    {

    }

    /// <summary>
    /// Sets destination to a random point.
    /// </summary>
    private void GotoNextPoint(NPCController owner)
    {
        if (owner.NavPoints.Count == 0)
            return;

        owner.Agent.SetDestination(owner.NavPoints[owner.DestPoint].position);
        owner.DestPoint = Random.Range(0, owner.NavPoints.Count);
    }
}
