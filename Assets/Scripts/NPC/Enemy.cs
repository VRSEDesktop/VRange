﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;
    /// <summary>
    /// Linked objects from NPC hand
    /// </summary>
    public GameObject Gun, Phone, BaseballBat, Axe;
    public bool isAgressive;

    private Animator animator;
    public bool IsDead { private set; get;}
    private NavMeshAgent navMeshAgent;

	private int hits;
	/// <summary>
	/// Max number of hits in torso the enemy can withstand
	/// </summary>
	public int maxHits;

	[System.Obsolete]
	public void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
		maxHits = Random.RandomRange(1, 2);
    }

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);
		if(hits == maxHits)
		{
			Die();
		}
        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HumanHead:
                if (!IsDead) Die();
            break;
            case HitboxType.HumanNeck:    break;
            case HitboxType.HumanPelvis:  break;

            case HitboxType.HumanSpine1: animator.Play("ShoulderHit"); hits++; break;
            case HitboxType.HumanSpine2: animator.Play("ShoulderHit"); hits++; break;
            case HitboxType.HumanSpine3: animator.Play("ShoulderHit"); hits++; break;

            case HitboxType.HumanThighLeft:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
				hits += 1;
				break;
            case HitboxType.HumanThighRight:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
				hits += 1;
				break;

            case HitboxType.HumanCalfLeft:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
				hits += 1;
				break;

            case HitboxType.HumanCalfRight:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
				hits += 1;
				break;

            case HitboxType.HumanFootLeft: break;
            case HitboxType.HumanFootRight: break;

            case HitboxType.HumanUpperArmLeft: animator.Play("ShoulderHit"); break;
            case HitboxType.HumanUpperArmRight:
				animator.Play("ShoulderHit");
                    break;

            case HitboxType.HumanLowerArmLeft: animator.Play("ShoulderHit"); break;
            case HitboxType.HumanLowerArmRight:
				animator.Play("ShoulderHit");
                break;

            case HitboxType.HumanHandLeft: animator.Play("ShoulderHit"); break;
            case HitboxType.HumanHandRight: animator.Play("ShoulderHit"); break;
        }

        Debug.Log("Enemy::OnHit() " + partHit.ToString());

        ScenarioLogs.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));
        return isAgressive ? HitType.RIGHT : HitType.UNWANTED;
    }

    /// <summary>
    /// Get HitboxType which was hit with the bulletHit
    /// </summary>
    /// <param name="bulletHit"></param>
    /// <returns></returns>
    private HitboxType GetHitboxTypeFromHit(RaycastHit raycastHit)
    {
        foreach (Hitbox hitbox in hitboxes)
        {
            if (hitbox.mesh == raycastHit.collider) return hitbox.type;
        }

        return HitboxType.None;
    }

    private void Die()
    {
        IsDead = true;
        animator.enabled = false;
        if (navMeshAgent) navMeshAgent.speed = 0f;
    }
}