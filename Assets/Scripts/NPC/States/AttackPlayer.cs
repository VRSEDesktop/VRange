using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : State<NPCController>
{
    private static AttackPlayer _instance;
    private GameObject Player;
    private GameObject Weapon;
    private float FieldOfView = 110f;
    private bool PlayerInSight;
    private CapsuleCollider Collider;
    /// <summary>
    /// Access to the script for the weapon
    /// </summary>
    private IWeapon WeaponController;

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
        Weapon = Object.Instantiate(owner.Weapon);
        WeaponController = Weapon.GetComponent<IWeapon>();

        //Initialize some scripted event here, need to think of how to implement that        
    }

    public override void ExitState(NPCController owner)
    {
        //Die
    }

    public override void Update(NPCController owner)
    {
        //Todo: add behaviour for attacking player
        
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
    /// Sets destination to a random point.
    /// </summary>
    private void MoveTowardsPlayer(NPCController owner)
    {
        
    }
}
