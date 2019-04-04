using UnityEngine;

public class ShootingTarget : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;
    public HitboxType Goal;

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);

        Debug.Log("ShootingTarrget::OnHit() " + partHit.ToString());
        Scenario.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));
        if(partHit == Goal)
        {
            return HitType.RIGHT;
        }
        switch (partHit)
        {
            case HitboxType.Hoofd:
            case HitboxType.Linkerarm:
            case HitboxType.Rechterarm:
            case HitboxType.Linkerbeen:
            case HitboxType.Rechterbeen:
            case HitboxType.Linkerhand:
            case HitboxType.Rechterhand:
            case HitboxType.Torso:
            case HitboxType.TARGET_TORSO_7:
            case HitboxType.TARGET_TORSO_8:
            case HitboxType.TARGET_TORSO_9:
            case HitboxType.TARGET_TORSO_10: return HitType.UNWANTED;
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