using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CapsuleCollider))]
public class AIController : MonoBehaviour
{
    /// <summary>
    /// List of Navigation points tagged in the editor with "Waypoint".
    /// </summary>
    [HideInInspector] public List<Transform> NavPoints = new List<Transform>();
    /// Index of the current destination in NavPoints.
    /// </summary>
    [HideInInspector] public int DestPoint = 0;
    [HideInInspector] public NavMeshAgent NavAgent;
    /// <summary>
    /// The item the NPC is wielding.
    /// </summary>
    [HideInInspector] public Weapon Item;
    /// <summary>
    /// The script for the item the NPC is wielding.
    /// </summary>
    [HideInInspector] public MonoBehaviour ItemScript;
    /// <summary>
    /// The class that manages the NPC's behaviour state.
    /// </summary>
    [HideInInspector] public StateMachine<AIController> StateMachine;
    /// <summary>
    /// Readonly value for NPCs FOV.
    /// </summary>
    public readonly float FieldOfView = 110f;
    /// <summary>
    /// Whether the NPC can see the Player.
    /// </summary>
    [HideInInspector] public bool PlayerInSight;
    /// <summary>
    /// The HitBox.
    /// </summary>
    [HideInInspector] public CapsuleCollider Collider;
    /// <summary>
    /// The current NavMesh the NPC is walking on. Only used in a State.
    /// </summary>
    [HideInInspector] public NavMeshHit CurrentNavMesh;

    protected void OnEnable()
    {
        // Setting up the statemachine
        StateMachine = new StateMachine<AIController>(this);
        StateMachine.ChangeState(Patrol.Instance);

        // Setting up the agent
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.autoBraking = false;

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
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
        StateMachine.OnTriggerStay(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        StateMachine.OnTriggerExit(this, other);
    }

    private void OnDrawGizmos()
    {
        if(StateMachine != null)
            UnityEditor.Handles.Label(gameObject.transform.position, StateMachine.ToString());
    }
}
