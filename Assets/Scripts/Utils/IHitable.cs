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
    NONE,

    HUMAN_PELVIS, HUMAN_NECK, HUMAN_HEAD,
    HUMAN_SPINE_1, HUMAN_SPINE_2, HUMAN_SPINE_3,
    HUMAN_THIGH_LEFT,       HUMAN_THIGH_RIGHT,
    HUMAN_CALF_LEFT,        HUMAN_CALF_RIGHT,
    HUMAN_FOOT_LEFT,        HUMAN_FOOT_RIGHT,
    HUMAN_UPPER_ARM_LEFT,   HUMAN_UPPER_ARM_RIGHT,
    HUMAN_LOWER_ARM_LEFT,   HUMAN_LOWER_ARM_RIGHT,
    HUMAN_HAND_LEFT,        HUMAN_HAND_RIGHT,

    TARGET_HEAD,
    TARGET_ARM_LEFT, TARGET_ARM_RIGHT,
    TARGET_LEG_LEFT, TARGET_LEG_RIGHT,
    TARGET_HAND_RIGHT, TARGET_HAND_LEFT,
    TARGET_TORSO_7, TARGET_TORSO_8, TARGET_TORSO_9, TARGET_TORSO_10,
}