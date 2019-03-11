using UnityEngine;

class ShootingTarget : MonoBehaviour, IHitable
{
    public ShootingTargetHitbox[] hitboxes;

    public void OnHit(BulletHit bulletHit)
    {
        ShootingTargetHitboxType partHit = GetHitboxTypeFromHit(bulletHit);

        int points = 0;

        switch(partHit)
        {
            case ShootingTargetHitboxType.HEAD: points = 8; break;

            case ShootingTargetHitboxType.LEFT_ARM: points = 4; break;
            case ShootingTargetHitboxType.LEFT_HAND: points = 5; break;
            case ShootingTargetHitboxType.LEFT_LEG: points = 0; break;
            case ShootingTargetHitboxType.RIGHT_ARM: points = 6; break;
            case ShootingTargetHitboxType.RIGHT_HAND: points = 9; break;
            case ShootingTargetHitboxType.RIGHT_LEG: points = 0; break;

            case ShootingTargetHitboxType.TORSO_7: points = 7; break;
            case ShootingTargetHitboxType.TORSO_8: points = 8; break;
            case ShootingTargetHitboxType.TORSO_9: points = 9; break;
            case ShootingTargetHitboxType.TORSO_10: points = 10; break;
        }

        Debug.Log(partHit.ToString() + " Points: " + points);

        //Scenario_Stats.RegisterHit(this, partHit, bulletHit);
    }

    /// <summary>
    /// Get HitboxType which was hit with the bulletHit
    /// </summary>
    /// <param name="bulletHit"></param>
    /// <returns></returns>
    private ShootingTargetHitboxType GetHitboxTypeFromHit(BulletHit bulletHit)
    {
        foreach (ShootingTargetHitbox hitbox in hitboxes)
        {
            if (hitbox.mesh == bulletHit.raycastHit.collider) return hitbox.type;
        }

        return ShootingTargetHitboxType.HEAD;
    }
}

[System.Serializable]
public struct ShootingTargetHitbox
{
    public Collider mesh;
    public ShootingTargetHitboxType type;
}

public enum ShootingTargetHitboxType
{
    HEAD,
    RIGHT_LEG, LEFT_LEG,
    RIGHT_ARM, LEFT_ARM,
    RIGHT_HAND, LEFT_HAND,
    TORSO_7, TORSO_8, TORSO_9, TORSO_10,
}