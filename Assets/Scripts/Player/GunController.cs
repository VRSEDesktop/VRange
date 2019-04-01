using UnityEngine;

/// <summary>
/// Player interface for controlling the gun
/// </summary>
public class GunController : HandController
{
    public Gun gun;
    private bool triggerPushed, reloadPushed;

    public override void HandleInput()
    {
        if (!triggerPushed && input.GetTriggerState()) PushTrigger();
        else if(triggerPushed && !input.GetTriggerState()) ReleaseTrigger();

        if (!reloadPushed && input.GetGripState()) PushReload();
        else if (reloadPushed && !input.GetGripState()) ReleaseReload();
    }

    public void Update()
    {     
        gun.gameObject.SetActive(input.IsControllerWorking());
    }

    private void PushTrigger()
    {
        if (triggerPushed) return;
        triggerPushed = true;
        gun.SendMessage("Use");
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
