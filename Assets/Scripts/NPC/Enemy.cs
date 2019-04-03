using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;

    private Animator animator;

    private bool isDead;

    private NavMeshAgent navMeshAgent;

    public void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);

        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HUMAN_HEAD:
                if (!isDead) Die();
                else Revive();
            break;
            case HitboxType.HUMAN_NECK:    break;
            case HitboxType.HUMAN_PELVIS:  break;

            case HitboxType.HUMAN_SPINE_1: break;
            case HitboxType.HUMAN_SPINE_2: break;
            case HitboxType.HUMAN_SPINE_3: break;

            case HitboxType.HUMAN_THIGH_LEFT:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
            break;
            case HitboxType.HUMAN_THIGH_RIGHT:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
            break;

            case HitboxType.HUMAN_CALF_LEFT:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true); break;
            case HitboxType.HUMAN_CALF_RIGHT:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true); break;

            case HitboxType.HUMAN_FOOT_LEFT: break;
            case HitboxType.HUMAN_FOOT_RIGHT: break;

            case HitboxType.HUMAN_UPPER_ARM_LEFT: break;
            case HitboxType.HUMAN_UPPER_ARM_RIGHT: break;

            case HitboxType.HUMAN_LOWER_ARM_LEFT: break;
            case HitboxType.HUMAN_LOWER_ARM_RIGHT: break;

            case HitboxType.HUMAN_HAND_LEFT: break;
            case HitboxType.HUMAN_HAND_RIGHT: break;
        }

        Debug.Log("Enemy::OnHit() " + partHit.ToString());

        Scenario.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));
        return HitType.RIGHT;
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

        return HitboxType.NONE;
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("Death2", true);
        navMeshAgent.speed = 0f;
    }

    //  tmp remove it
    private void Revive()
    {
        isDead = false;
        animator.SetBool("Death2", false);
        animator.SetBool("GetUp", true);
        navMeshAgent.speed = 1.5f;
    }
}