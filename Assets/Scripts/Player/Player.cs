using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Saves data relevant to the player.
    /// </summary>
    public class Player : MonoBehaviour
    {
        public Vector3 playerHeight = new Vector3(0, 1.64f);
        public static Player Instance { get; private set; }

        public VR_Controller leftHandInput, rightHandInput;
        public Gun leftGun, rightGun;
        private GunInterface leftHand, rightHand;

        private void Start()
        {
            leftHand = new GunInterface(leftGun);
            rightHand = new GunInterface(rightGun);
        }

        private void Awake()
        {
            Instance = this;
        }

        public void Update()
        {
            if(leftHandInput != null) leftHand.HandleInput(leftHandInput.GetTriggerState());
            if(rightHandInput != null) rightHand.HandleInput(rightHandInput.GetTriggerState());
        }
    }
}