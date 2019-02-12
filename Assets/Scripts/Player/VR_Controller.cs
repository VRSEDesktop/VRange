using UnityEngine;
using Valve.VR;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Set the different button presses of a VR controller.
    /// </summary>
    public class VR_Controller : MonoBehaviour
    {
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean triggerAction;

        public bool GetTrigger()
        {
            return triggerAction.GetState(handType);
        }

        public void Update()
        {
            bool value = GetTrigger();
            if (value) print("Trigger state:" + value);
        }
    }
}