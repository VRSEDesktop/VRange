using Assets.Scripts.Player;
using UnityEngine;

/// <summary>
/// Player interface for controlling the gun
/// </summary>
public class GunInterface : MonoBehaviour
{
    private Gun gun;
    private bool trigerPushed, reloadPushed;
    
    public GunInterface(Gun gun)
    {
        this.gun = gun;
    }

    public void HandleInput(VR_Controller input)
    {
        if (!trigerPushed && input.GetTriggerState()) PushTriger();
        else if(trigerPushed && !input.GetTriggerState()) ReleaseTriger();

        if (!reloadPushed && input.GetGripState()) PushReload();
        else if (reloadPushed && !input.GetGripState()) ReleaseReload();
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

    private void PushReload()
    {
        reloadPushed = true;
        gun.SendMessage("Reload");
    }

    private void ReleaseReload()
    {
        reloadPushed = false;
    }
}
