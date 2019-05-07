using UnityEngine;

/// <summary>
/// Player interface for controlling the gun
/// </summary>
public class GunController : HandController
{
    private static readonly float DoubleClickDelay = 0.35f;

    public Gun gun;
    private bool triggerPushed, reloadPushed;
    private float LastTime;

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
		//Debug.Log(triggerPushed + " " + Time.realtimeSinceStartup);
		if (triggerPushed || Time.realtimeSinceStartup - LastTime <= DoubleClickDelay) return;
        triggerPushed = true;
        LastTime = Time.realtimeSinceStartup;
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
