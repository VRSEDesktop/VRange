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

    public void OnHit(BulletHit bulletHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(bulletHit);

        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HEAD:

                if (!isDead) Die();
                else Revive();
            break;

            case HitboxType.TORSO:
                break;

            case HitboxType.LEFT_LEG:
                navMeshAgent.speed = 0.5f;
                break;

            case HitboxType.RIGHT_LEG:
                navMeshAgent.speed = 0.5f;
                break;
        }

        Debug.Log("Enemy::OnHit() " + partHit.ToString());

        Scenario.logs.Add(new LoggedHit(this, partHit, bulletHit));
    }

    /// <summary>
    /// Get HitboxType which was hit with the bulletHit
    /// </summary>
    /// <param name="bulletHit"></param>
    /// <returns></returns>
    private HitboxType GetHitboxTypeFromHit(BulletHit bulletHit)
    {
        foreach (Hitbox hitbox in hitboxes)
        {
            if (hitbox.mesh == bulletHit.RaycastHit.collider) return hitbox.type;
        }

        return HitboxType.HEAD;
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