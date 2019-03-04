using Assets.Scripts.Player;
using UnityEngine;

/// <summary>
/// Player interface for controlling the gun
/// </summary>
public class GunInterface : MonoBehaviour
{
    private Gun gun;
    private bool triggerPushed, reloadPushed;
    
    public GunInterface(Gun gun)
    {
        this.gun = gun;
    }

    public void HandleInput(VR_Controller input)
    {
        if (!triggerPushed && input.GetTriggerState()) PushTrigger();
        else if(triggerPushed && !input.GetTriggerState()) ReleaseTrigger();

        if (!reloadPushed && input.GetGripState()) PushReload();
        else if (reloadPushed && !input.GetGripState()) ReleaseReload();
    }

    private void PushTrigger()
    {
        if (triggerPushed)
            return;
        gun.SendMessage("Shoot");
        triggerPushed = true;
    }

    private void ReleaseTrigger()
    {
        triggerPushed = false;
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
