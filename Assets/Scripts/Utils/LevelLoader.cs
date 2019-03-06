using UnityEngine;
using Valve.VR;

class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Temporary class without much functionalities
    /// </summary>
    void Start()
    {
        GetComponent<SteamVR_LoadLevel>().levelName = "Exercise_2";
        GetComponent<SteamVR_LoadLevel>().Trigger();
    }
}
