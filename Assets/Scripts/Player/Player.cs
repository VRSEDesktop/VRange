using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Saves data relevant to the player.
    /// </summary>
    public class Player : MonoBehaviour, IHitable
    {
        public Vector3 playerHeight = new Vector3(0, 1.64f);
        public static Player Instance { get; private set; }

        public VR_Controller leftHandInput, rightHandInput;
        public Gun leftGun, rightGun;
        private GunInterface leftHand, rightHand;

        public Hitbox hitbox;

        private void Start()
        {
            if (leftGun != null) leftHand = new GunInterface(leftGun);
            if (rightGun != null) rightHand = new GunInterface(rightGun);
        }

        private void Awake()
        {
            Instance = this;
        }

        public void Update()
        {
            if (leftHandInput != null)
            {
                leftHand.HandleInput(leftHandInput);
                leftGun.gameObject.SetActive(leftHandInput.IsControllerWorking());
            }
            if (rightHandInput != null)
            {
                rightHand.HandleInput(rightHandInput);
                rightGun.gameObject.SetActive(rightHandInput.IsControllerWorking());
            }
        }

        public void OnHit(BulletHit bulletHit)
        {
            Debug.Log("PLAYER WAS SHOT");
        }

        void OnGUI()
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Shot count: " + (leftGun.shotsFired + rightGun.shotsFired));

            for (int i = 0; i < Scenario_Stats.hits.Count; i++)
            {
                GUI.Label(new Rect(Screen.width / 12, Screen.height / 24 * i, Screen.width / 4 * 2, Screen.height / 6), Scenario_Stats.hits[i].part.ToString());
            }
        }
    }
}