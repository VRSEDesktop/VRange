using UnityEngine;
using Valve.VR;

class LevelLoader : MonoBehaviour
{
    void Start()
    {
        GetComponent<SteamVR_LoadLevel>().levelName = "Exercise_2";
        GetComponent<SteamVR_LoadLevel>().Trigger();
    }
}
