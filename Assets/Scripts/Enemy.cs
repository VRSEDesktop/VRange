﻿using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;

    private Animator animator;

    private bool isDead;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnHit(BulletHit bulletHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(bulletHit);

        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HEAD:

                Debug.Log("HIT HEAD");
                if (!isDead) Die();
                else Revive();
            break;

            case HitboxType.TORSO:
                Debug.Log("HIT TORSO");
                break;
        }

        Scenario_Stats.RegisterHit(this, partHit, bulletHit);
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
            if (hitbox.mesh == bulletHit.raycastHit.collider) return hitbox.type;
        }

        return HitboxType.HEAD;
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("Death2", true);
    }

    //  tmp remove it
    private void Revive()
    {
        isDead = false;
        animator.SetBool("Death2", false);
        animator.SetBool("GetUp", true);
    }
}

[System.Serializable]
public struct Hitbox
{
    public Collider mesh;
    public HitboxType type;
}

public enum HitboxType
{
    HEAD,
    TORSO,
    LEFT_ARM,
    RIGHT_ARM,
    LEFT_LEG,
    RIGHT_LEG
}