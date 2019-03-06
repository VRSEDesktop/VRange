using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [HideInInspector]
    public List<Transform> NavPoints = new List<Transform>();
    [HideInInspector]
    public int DestPoint = 0;
    [HideInInspector]
    public NavMeshAgent Agent;
    [HideInInspector]
    public GameObject Weapon;
    [HideInInspector]
    public StateMachine<NPCController> StateMachine;

    private void Awake()
    {
        StateMachine = new StateMachine<NPCController>(this);
        StateMachine.ChangeState(Patrol.Instance);
    }

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;

        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            NavPoints.Add(o.transform);
        }
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        StateMachine.OnTriggerStay(other);
    }

    private void OnDrawGizmos()
    {
        if(StateMachine != null)
            UnityEditor.Handles.Label(gameObject.transform.position, StateMachine.ToString());
    }
}
