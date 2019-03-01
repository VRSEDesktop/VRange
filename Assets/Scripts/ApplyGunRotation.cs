using UnityEngine;

class ApplyGunRotation : MonoBehaviour
{
    public Gun gun;
    public bool normalGun = false;

    void Start()
    {
        if (normalGun)  ApplyRealGunRotation();
        else            ApplyControllerRotation();
    }

    private void ApplyControllerRotation()
    {     
        gun.transform.rotation = Quaternion.Euler(-45, -180, 0);
        gun.transform.position += new Vector3(0, -0.05f, -0.05f);
        Debug.Log("Controler config");
    }

    // TODO
    private void ApplyRealGunRotation()
    {
        gun.transform.rotation = Quaternion.Euler(0, 0, 0);
        gun.transform.position = new Vector3();
    }
}
