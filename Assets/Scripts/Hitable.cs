using UnityEngine;

public interface IHitable
{
    void OnHit(BulletHit bulletHit);
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
    NONE
}