using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;

    public void OnHit(BulletHit bulletHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(bulletHit);

        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HEAD:
                Debug.Log("HIT HEAD");
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

        return HitboxType.NONE;
    }
}
