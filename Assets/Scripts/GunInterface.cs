using UnityEngine;

/// <summary>
/// Player interface for controlling the gun
/// </summary>
public class GunInterface : MonoBehaviour
{
    private Gun gun;
    private bool trigerPushed;
    
    public GunInterface(Gun gun)
    {
        this.gun = gun;
    }

    public void HandleInput(bool state)
    {
        if (!trigerPushed && state) PushTriger();
        else if(trigerPushed && !state) ReleaseTriger();
    }

    private void PushTriger()
    {
        trigerPushed = true;
        gun.SendMessage("Shoot");
    }

    private void ReleaseTriger()
    {
        trigerPushed = false;
    }
}
