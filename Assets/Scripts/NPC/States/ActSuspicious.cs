using UnityEngine;

namespace Assets.Scripts.NPC.States
{
    class ActSuspicious : State<NPCController>
    {
        private static ActSuspicious _instance;

        public ActSuspicious()
        {
            if (_instance != null)
                return;

            _instance = this;
        }

        public static ActSuspicious Instance {
            get
            {
                if (_instance == null)
                    new ActSuspicious();
                return _instance;
            }
        }

        public override void EnterState(NPCController owner)
        {

        }

        public override void ExitState(NPCController owner)
        {

        }

        public override void OnTriggerExit(NPCController owner, Collider other)
        {

        }

        public override void OnTriggerStay(NPCController owner, Collider other)
        {

        }

        public override void Update(NPCController owner)
        {

        }
    }
}
