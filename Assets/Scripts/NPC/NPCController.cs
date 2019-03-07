using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    /// <summary>
    /// List of Navigation points tagged in the editor with "Waypoint".
    /// </summary>
    [HideInInspector]
    public List<Transform> NavPoints = new List<Transform>();
    /// <summary>
    /// Index of the current destination in NavPoints.
    /// </summary>
    [HideInInspector]
    public int DestPoint = 0;
    [HideInInspector]
    public NavMeshAgent Agent;
    /// <summary>
    /// The weapon the NPC is wielding.
    /// </summary>
    [HideInInspector]
    public GameObject Weapon;
    /// <summary>
    /// The class that manages the NPC's behaviour state.
    /// </summary>
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

    /// <summary>
    /// Draw some information in the editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        if(StateMachine != null)
            UnityEditor.Handles.Label(gameObject.transform.position, StateMachine.ToString());
    }
}
