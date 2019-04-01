using UnityEngine;

public interface IHitable
{
    HitType OnHit(Gun gun, RaycastHit raycastHit);
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
    RIGHT_LEG,

    // Shooting target hitboxes
    RIGHT_HAND, LEFT_HAND,
    TORSO_7, TORSO_8, TORSO_9, TORSO_10,
}