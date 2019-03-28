using UnityEngine;

public class Patrol : State<AIController>
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
        // Checks if the the npc has reached the target
        if (!owner.NavAgent.pathPending && owner.NavAgent.remainingDistance < 0.5f)
            GotoNextPoint(owner); // Triggers function to set the new waypoint
    }

    /// <summary>
    /// Sets destination to a random point.
    /// </summary>
    private void GotoNextPoint(AIController owner)
    {
        // Checks if there are waypoints
        if (owner.NavPoints.Count == 0)
            return;

        // Setting new target
        owner.NavAgent.SetDestination(owner.NavPoints[owner.DestPoint].position);
        owner.DestPoint = Random.Range(0, owner.NavPoints.Count);
    }
}
