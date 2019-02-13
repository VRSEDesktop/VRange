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
        public AudioSource audioPlayer;

        private bool release;

        public bool GetTrigger()
        {
            return triggerAction.GetState(handType);
        }

        public void Update()
        {
            bool value = GetTrigger();
            if (value)
            {
                if (release == false)
                {
                    print("Trigger state:" + value);
                    audioPlayer.Play();
                    release = true;
                }
            } else
            {
                release = false;
            }
            
        }
    }
}