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
            case HitboxType.HEAD:
            case HitboxType.LEFT_ARM:
            case HitboxType.LEFT_HAND:
            case HitboxType.LEFT_LEG:
            case HitboxType.RIGHT_ARM:
            case HitboxType.RIGHT_HAND:
            case HitboxType.RIGHT_LEG:
            case HitboxType.TORSO_7:
            case HitboxType.TORSO_8:
            case HitboxType.TORSO_9:
            case HitboxType.TORSO_10: return HitType.RIGHT;
            default: return HitType.MISS;
        }
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

        return HitboxType.HEAD;
    }
}