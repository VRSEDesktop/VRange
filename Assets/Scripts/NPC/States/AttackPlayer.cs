﻿using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : State<AIController>
{ 
    private static AttackPlayer _instance;
    private ShouldSurrender ShouldSurrender = new ShouldSurrender();
    private ShouldMoveToCover ShouldMoveToCover = new ShouldMoveToCover();

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

    public override void EnterState(AIController owner)
    {
        owner.Player = GameObject.FindGameObjectWithTag("Player");

        owner.Agent.SetDestination(owner.Player.transform.position);
    }

    public override void ExitState(AIController owner)
    {
        
    }

    public override void Update(AIController owner)
    {
        SetWeaponPosition(owner);
        SetMovement(owner);
        SetState(owner);

        AttackWithGun(owner);
    }

    private void AttackWithGun(AIController owner)
    {
        bool hasShot = owner.Item.Use();

        if(!hasShot)
        {
            ReloadGun(owner);
        }
    }

    private void ReloadGun(AIController owner)
    {
        ((Gun)owner.Item).Reload();
    }

    public override void OnTriggerStay(AIController owner, Collider other)
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

    public override void OnTriggerExit(AIController owner, Collider other)
    {
        owner.PlayerInSight = !(other.gameObject == owner.Player);
    }

    /// <summary>
    /// Place the weapon in the NPC's hand.
    /// </summary>
    private void SetWeaponPosition(AIController owner)
    {
        
    }

    /// <summary>
    /// Logic of the NPC's movement, aim and animations.
    /// </summary>
    private void SetMovement(AIController owner)
    {
        float distanceToPlayer = Vector3.Distance(owner.Player.transform.position, owner.transform.position);

        //Reached a destination
        if(owner.Agent.pathStatus == NavMeshPathStatus.PathComplete)
        {

            owner.Agent.SamplePathPosition(NavMesh.AllAreas, 0f, out owner.CurrentNavMesh);
        }
    }

    /// <summary>
    /// Logic for changing the state of the NPC's statemachine.
    /// </summary>
    private void SetState(AIController owner)
    {
        //Someting about chances to surrender, dying, etc. in here. Perhaps something with level of fear and level of agression.
        if (ShouldSurrender.Decide(owner))
            owner.StateMachine.ChangeState(Surrender.Instance);

        if(ShouldMoveToCover.Decide(owner))
        {
            //Find nearest cover
            //owner.Agent.SetDestination();
        }

        switch (owner.CurrentNavMesh.mask)
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
            //User 3
            case 3:
                owner.StateMachine.ChangeState(TakeCover.Instance);
                break;
        }
    }
}
