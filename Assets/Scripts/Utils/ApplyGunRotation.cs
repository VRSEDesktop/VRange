using UnityEngine;

/// <summary>
/// Script used to adjuct a transform of a controller
/// </summary>
public class ApplyGunRotation : MonoBehaviour
{
    public Gun Gun;
    public bool NormalGun = false;

    public void Start()
    {
        if (NormalGun)  ApplyRealGunRotation();
        else            ApplyControllerRotation();
    }

    private void ApplyControllerRotation()
    {     
        Gun.transform.rotation = Quaternion.Euler(-45, -180, 0);
        Gun.transform.position += new Vector3(0, -0.05f, -0.05f);
    }

    private void ApplyRealGunRotation()
    {
        Gun.transform.rotation = Quaternion.Euler(0, -88, 90);
        Gun.transform.position += new Vector3(0.05f, 0, -0.1f);
    } 
}
