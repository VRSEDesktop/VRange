using UnityEngine;
using Valve.VR;

/// <summary>
/// Gets inputs from the controller
/// </summary>
public class VR_Controller : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean triggerAction;
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Pose position;

    public bool GetTriggerState()
    {
        return triggerAction.GetState(handType);
    }

    public bool GetGripState()
    {
        return gripAction.GetState(handType);
    }

    public bool IsControllerWorking()
    {
        Vector3 pos = position.GetLocalPosition(handType);

        if (pos == null || pos.Equals(Vector3.zero)) return false;
        return true;
    }
}