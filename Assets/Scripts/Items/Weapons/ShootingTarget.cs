using UnityEngine;

public class ShootingTarget : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;

    public void OnHit(BulletHit bulletHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(bulletHit);

        switch(partHit)
        {
            case HitboxType.HEAD: break;

            case HitboxType.LEFT_ARM: break;
            case HitboxType.LEFT_HAND: break;
            case HitboxType.LEFT_LEG: break;
            case HitboxType.RIGHT_ARM: break;
            case HitboxType.RIGHT_HAND: break;
            case HitboxType.RIGHT_LEG: break;

            case HitboxType.TORSO_7:  break;
            case HitboxType.TORSO_8: break;
            case HitboxType.TORSO_9: break;
            case HitboxType.TORSO_10: break;
        }

        Debug.Log("ShootingTarrget::OnHit() " + partHit.ToString());

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
}