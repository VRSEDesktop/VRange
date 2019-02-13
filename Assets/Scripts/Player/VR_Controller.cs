using UnityEngine;
using Valve.VR;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Gets inputs from the controller
    /// </summary>
    public class VR_Controller : MonoBehaviour
    {
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean triggerAction;
        public SteamVR_Action_Boolean gripAction;

        public bool GetTriggerState()
        {
            return triggerAction.GetState(handType);
        }

        public bool GetGripState()
        {
            return gripAction.GetState(handType);
        }
    }
}