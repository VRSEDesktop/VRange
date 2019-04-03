using UnityEngine;

public class ShootingTarget : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);

        Debug.Log("ShootingTarrget::OnHit() " + partHit.ToString());
        Scenario.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));

        switch (partHit)
        {
            case HitboxType.TARGET_HEAD:
            case HitboxType.TARGET_ARM_LEFT:
            case HitboxType.TARGET_ARM_RIGHT:
            case HitboxType.TARGET_LEG_LEFT:
            case HitboxType.TARGET_LEG_RIGHT:
            case HitboxType.TARGET_HAND_LEFT:
            case HitboxType.TARGET_HAND_RIGHT:
            case HitboxType.TARGET_TORSO_7:
            case HitboxType.TARGET_TORSO_8:
            case HitboxType.TARGET_TORSO_9:
            case HitboxType.TARGET_TORSO_10: return HitType.RIGHT;
        }

        return HitType.MISS;
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
}