using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CapsuleCollider))]
public class NPCController : MonoBehaviour
{
    /// <summary>
    /// List of Navigation points tagged in the editor with "Waypoint".
    /// </summary>
    [HideInInspector] public List<Transform> NavPoints = new List<Transform>();
    /// <summary>
    /// Index of the current destination in NavPoints.
    /// </summary>
    [HideInInspector] public int DestPoint = 0;
    [HideInInspector] public NavMeshAgent Agent;
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
    [HideInInspector] public StateMachine<NPCController> StateMachine;
    /// <summary>
    /// Access to the Player GameObject
    /// </summary>
    [HideInInspector] public GameObject Player;
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
    /// The level of agression.
    /// </summary>
    public float LevelOfAgression;
    /// <summary>
    /// The strategic competence of the NPC.
    /// </summary>
    public float LevelOfTactics;
    /// <summary>
    /// The animator for the animations
    /// </summary>
    public Animator Anim;
    /// <summary>
    /// The rigidbody for detecting speed
    /// </summary>
    public Rigidbody Rig;

    private void OnEnable()
    {
        // Setting up the statemachine
        StateMachine = new StateMachine<NPCController>(this);
        StateMachine.ChangeState(Patrol.Instance);

        // Setting up the agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            NavPoints.Add(o.transform);
        }

        LevelOfAgression = Random.Range(0, 100);
        LevelOfTactics = Random.Range(0, 100);
    }

    private void Update()
    {
        StateMachine.Update();
        Anim.SetFloat("Speed", Rig.velocity.magnitude);
        Debug.Log(Rig.velocity.magnitude + " ");
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
