using UnityEngine;
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
			case HitboxType.HumanHead:
                if (!isDead) Die();
                //else Revive();
            break;
            case HitboxType.HumanNeck:    break;
            case HitboxType.HumanPelvis:  break;

            case HitboxType.HumanSpine1: break;
            case HitboxType.HumanSpine2: break;
            case HitboxType.HumanSpine3: break;

            case HitboxType.HumanThighLeft:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
            break;
            case HitboxType.HumanThighRight:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true);
            break;

            case HitboxType.HumanCalfLeft:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true); break;
            case HitboxType.HumanCalfRight:
                if (navMeshAgent) navMeshAgent.speed = 0.5f;
                animator.SetBool("Limbing", true); break;

            case HitboxType.HumanFootLeft: break;
            case HitboxType.HumanFootRight: break;

            case HitboxType.HumanUpperArmLeft: break;
            case HitboxType.HumanUpperArmRight:
                animator.SetBool("Shoulder", true);
                    break;

            case HitboxType.HumanLowerArmLeft: break;
            case HitboxType.HumanLowerArmRight:
                animator.SetBool("Shoulder", true);
                break;

            case HitboxType.HumanHandLeft: break;
            case HitboxType.HumanHandRight: break;
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
        isDead = true;
        animator.enabled = false;
        if (navMeshAgent) navMeshAgent.speed = 0f;
    }

    //  tmp remove it
    private void Revive()
    {
        isDead = false;
        animator.enabled = true;
        //animator.SetBool("Death2", false);
        animator.SetBool("GetUp", true);
        if(navMeshAgent) navMeshAgent.speed = 1.5f;
    }
}