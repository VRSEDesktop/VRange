using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private List<Transform> navPoints = new List<Transform>();
    private int destPoint = 0;
    private NavMeshAgent agent;
    private StateMachine<NPCController> StateMachine;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            navPoints.Add(o.transform);
        }
        
        StateMachine = new StateMachine<NPCController>(this);
        StateMachine.ChangeState(Patrol.Instance);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        

        StateMachine.Update();
    }

    /// <summary>
    /// Sets destination to a random point.
    /// </summary>
    private void GotoNextPoint()
    {
        if (navPoints.Count == 0)
            return;

        agent.SetDestination(navPoints[destPoint].position);
        destPoint = Random.Range(0, navPoints.Count);
    }
}
