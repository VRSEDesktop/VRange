using System.Collections;
using UnityEngine;

public class ShootingTarget : ExcersiseState, IHitable
{
    public Hitbox[] hitboxes;
    public HitboxType Goal;

	public GameObject step;

	public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {

        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);
		step.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);

		Debug.Log("ShootingTarrget::OnHit() " + partHit.ToString());
        ScenarioLogs.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));
        if(partHit == Goal)
        {
            return HitType.RIGHT;
        }
        switch (partHit)
        {
            case HitboxType.TargetHead:
            case HitboxType.TargetArmLeft:
            case HitboxType.TargetArmRight:
            case HitboxType.TargetLegLeft:
            case HitboxType.TargetLegRight:
            case HitboxType.TargetHandLeft:
            case HitboxType.TargetHandRight:
            case HitboxType.TargetTorso: return HitType.UNWANTED;
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

        return HitboxType.None;
    }

}