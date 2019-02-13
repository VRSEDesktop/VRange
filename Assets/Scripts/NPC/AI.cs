using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public StateMachine<AI> StateMachine;
    public Transform[] NavPoints;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        
        StateMachine = new StateMachine<AI>(this);
        StateMachine.ChangeState(Patrol.Instance);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        

        StateMachine.Update();
    }

    /// <summary>
    /// Sets destination to a random point
    /// </summary>
    private void GotoNextPoint()
    {
        if (NavPoints.Length == 0)
            return;

        agent.destination = NavPoints[destPoint].position;
        destPoint = Random.Range(0, NavPoints.Length);
    }
}
