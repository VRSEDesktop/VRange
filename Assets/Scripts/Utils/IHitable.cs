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
    None,

    [Description("Torso")]
    HumanPelvis,
    [Description("Hoofd")]
    HumanNeck,
    [Description("Hoofd")]
    HumanHead,

    [Description("Torso")]
    HumanSpine1,
    [Description("Torso")]
    HumanSpine2,
    [Description("Torso")]
    HumanSpine3,

    [Description("Linkerbeen")]
    HumanThighLeft,
    [Description("Rechterbeen")]
    HumanThighRight,
    [Description("Linkerbeen")]
    HumanCalfLeft,
    [Description("Rechterbeen")]
    HumanCalfRight,
    [Description("Linkerbeen")]
    HumanFootLeft,
    [Description("Rechterbeen")]
    HumanFootRight,

    [Description("Linkerarm")]
    HumanUpperArmLeft,
    [Description("Rechterarm")]
    HumanUpperArmRight,
    [Description("Linkerarm")]
    HumanLowerArmLeft,
    [Description("Rechterarm")]
    HumanLowerArmRight,
    [Description("Linkerarm")]
    HumanHandLeft,
    [Description("Rechterarm")]
    HumanHandRight,

    [Description("Hoofd")]
    TargetHead,
    [Description("Linkerarm")]
    TargetArmLeft,
    [Description("Rechterarm")]
    TargetArmRight,
    [Description("Linkerbeen")]
    TargetLegLeft,
    [Description("Rechterbeen")]
    TargetLegRight,
    [Description("Linkerarm")]
    TargetHandLeft,
    [Description("Rechterarm")]
    TargetHandRight,
    [Description("Torso")]
    TargetTorso,
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