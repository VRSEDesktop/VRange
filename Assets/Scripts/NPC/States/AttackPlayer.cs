using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Weapons.Abstraction;
using UnityEngine.AI;

public class AttackPlayer : State<NPCController>
{
    private static AttackPlayer _instance;
    private GameObject Player;
    private GameObject Weapon;
    private readonly float FieldOfView = 110f;
    private bool PlayerInSight;
    private CapsuleCollider Collider;

    /// <summary>
    /// Access to the script for the weapon.
    /// </summary>
    private Weapon WeaponController;

    private AttackPlayer()
    {
        if(_instance != null)
            return;

        _instance = this;
    }

    public static AttackPlayer Instance
    {
        get
        {
            if (_instance == null)
                new AttackPlayer();
            return _instance;
        }
    }

    public override void EnterState(NPCController owner)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Collider = owner.GetComponent<CapsuleCollider>();

        //Start agression (draw weapon or whatever)
        Weapon = GameObject.Instantiate(owner.Weapon, owner.transform);
        WeaponController = Weapon.GetComponent<Weapon>();
        
    }

    public override void ExitState(NPCController owner)
    {
        //Die
    }

    public override void Update(NPCController owner)
    {
        SetWeaponPosition(owner);
        SetMovement(owner);
        SetState(owner);
    }

    public override void OnTriggerStay(NPCController owner, Collider other)
    {
        if(other.gameObject == Player)
        {
            PlayerInSight = false;
            Vector3 direction = other.transform.position - owner.transform.position;
            float angle = Vector3.Angle(direction, owner.transform.forward);

            //Is player in the field of view of the NPC
            if(angle < FieldOfView * 0.5f)
            {
                //Are there any objects obstructing the view
                RaycastHit hit;
                if(Physics.Raycast(owner.transform.position = owner.transform.up, direction.normalized, out hit, Collider.radius))
                {
                    if(hit.collider.gameObject == Player)
                    {
                        PlayerInSight = true;
                    }
                }
            }
        }
    }

    public override void OnTriggerExit(NPCController owner, Collider other)
    {
        if (other.gameObject == Player)
            PlayerInSight = false;
    }

    /// <summary>
    /// Place the weapon in the NPC's hand.
    /// </summary>
    private void SetWeaponPosition(NPCController owner)
    {
        
    }

    /// <summary>
    /// Logic of the NPC's movement and animations.
    /// </summary>
    private void SetMovement(NPCController owner)
    {


        //Logic for animation

        //Reached a destination
        if(owner.Agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            NavMeshHit navMeshHit;
            if (owner.Agent.SamplePathPosition(NavMesh.AllAreas, 0f, out navMeshHit))
            {
                switch(navMeshHit.mask)
                {
                    //Walkable
                    case 0:
                        break;
                    //Not Walkable
                    case 1:
                        break;
                    //Jump
                    case 2:
                        break;
                    //User 3 (Cover)
                    case 3:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Logic for changing the state of the NPC's statemachine.
    /// </summary>
    private void SetState(NPCController owner)
    {
        //Someting about chances to surrender, dying, etc. in here. Perhaps something with level of fear and level of agression.
    }
}
