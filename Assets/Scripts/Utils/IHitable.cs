using System.ComponentModel;
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
    [Description("Mis")]
    NONE,

    [Description("Torso")]
    HUMAN_PELVIS,
    [Description("Hoofd")]
    HUMAN_NECK,
    [Description("Hoofd")]
    HUMAN_HEAD,

    [Description("Torso")]
    HUMAN_SPINE_1,
    [Description("Torso")]
    HUMAN_SPINE_2,
    [Description("Torso")]
    HUMAN_SPINE_3,

    [Description("Linkerbeen")]
    HUMAN_THIGH_LEFT,
    [Description("Rechterbeen")]
    HUMAN_THIGH_RIGHT,
    [Description("Linkerbeen")]
    HUMAN_CALF_LEFT,
    [Description("Rechterbeen")]
    HUMAN_CALF_RIGHT,
    [Description("Linkerbeen")]
    HUMAN_FOOT_LEFT,
    [Description("Rechterbeen")]
    HUMAN_FOOT_RIGHT,

    [Description("Linkerarm")]
    HUMAN_UPPER_ARM_LEFT,
    [Description("Rechterarm")]
    HUMAN_UPPER_ARM_RIGHT,
    [Description("Linkerarm")]
    HUMAN_LOWER_ARM_LEFT,
    [Description("Rechterarm")]
    HUMAN_LOWER_ARM_RIGHT,
    [Description("Linkerarm")]
    HUMAN_HAND_LEFT,
    [Description("Rechterarm")]
    HUMAN_HAND_RIGHT,

    [Description("Hoofd")]
    TARGET_HEAD,
    [Description("Linkerarm")]
    TARGET_ARM_LEFT,
    [Description("Rechterarm")]
    TARGET_ARM_RIGHT,
    [Description("Linkerbeen")]
    TARGET_LEG_LEFT,
    [Description("Rechterbeen")]
    TARGET_LEG_RIGHT,
    [Description("Linkerarm")]
    TARGET_HAND_LEFT,
    [Description("Rechterarm")]
    TARGET_HAND_RIGHT,
    [Description("Torso")]
    TARGET_TORSO,
}

public static class EnumUtils
{
    public static string ToDescriptionString(this HitboxType val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}