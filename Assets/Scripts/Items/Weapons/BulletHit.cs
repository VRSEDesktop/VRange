using UnityEngine;

/// <summary>
/// Wrapper class for the bullet hitting the objects
/// </summary>
public class BulletHit : MonoBehaviour
{
    public Gun Gun { get; }
    public RaycastHit RaycastHit { get; }

    public BulletHit(Gun gun, RaycastHit raycastHit)
    {
        Gun = gun;
        RaycastHit = raycastHit;
    }
}

/// <summary>
/// Enums for the feedback, right for proper hit in the suspect, unwanted for civilians etc.
/// </summary>
public enum HitType
{
    MISS, UNWANTED, RIGHT 
}
