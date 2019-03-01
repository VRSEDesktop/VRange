using UnityEngine;

/// <summary>
/// Wrapper class for the bullet hitting the objects
/// </summary>
public class BulletHit : MonoBehaviour
{
    public Gun gun { get; }
    public RaycastHit raycastHit { get; }

    public BulletHit(Gun gun, RaycastHit raycastHit)
    {
        this.gun = gun;
        this.raycastHit = raycastHit;
    }
}
