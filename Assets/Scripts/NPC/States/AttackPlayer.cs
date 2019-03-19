using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Weapons.Abstraction;
using UnityEngine.AI;
using Assets.Scripts.StateMachine;

public class AttackPlayer : State<NPCController>
{ 
    private static AttackPlayer _instance;
    private ShouldSurrender ShouldSurrender = new ShouldSurrender();

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
        owner.Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void ExitState(NPCController owner)
    {
        
    }

    public override void Update(NPCController owner)
    {
        SetWeaponPosition(owner);
        SetMovement(owner);
        SetState(owner);
    }

    public override void OnTriggerStay(NPCController owner, Collider other)
    {
        if(other.gameObject == owner.Player)
        {
            owner.PlayerInSight = false;
            Vector3 direction = other.transform.position - owner.transform.position;
            float angle = Vector3.Angle(direction, owner.transform.forward);

            //Is player in the field of view of the NPC
            if(angle < owner.FieldOfView * 0.5f)
            {
                //Are there any objects obstructing the view
                if (Physics.Raycast(owner.transform.position = owner.transform.up, direction.normalized, out RaycastHit hit, owner.Collider.radius))
                {
                    owner.PlayerInSight = hit.collider.gameObject == owner.Player;
                }
            }
        }
    }

    public override void OnTriggerExit(NPCController owner, Collider other)
    {
        owner.PlayerInSight = !(other.gameObject == owner.Player);
    }

    /// <summary>
    /// Place the weapon in the NPC's hand.
    /// </summary>
    private void SetWeaponPosition(NPCController owner)
    {
        
    }

    /// <summary>
    /// Logic of the NPC's movement, aim and animations.
    /// </summary>
    private void SetMovement(NPCController owner)
    {
        owner.Agent.SetDestination(owner.Player.transform.position);
        //Reached a destination
        if(owner.Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if (owner.Agent.SamplePathPosition(NavMesh.AllAreas, 0f, out NavMeshHit navMeshHit))
            {
                switch (navMeshHit.mask)
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
        if (ShouldSurrender.Decide(owner))
            owner.StateMachine.ChangeState(Surrender.Instance);
    }
}
