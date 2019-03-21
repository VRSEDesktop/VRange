using TMPro;
using UnityEngine;
using Valve.VR;

public class LoadLevelButton : MonoBehaviour, IHitable
{
    public string levelName;

    public void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = name;
    }

    public void OnHit(BulletHit bulletHit)
    {
        GetComponent<SteamVR_LoadLevel>().levelName = levelName;
        GetComponent<SteamVR_LoadLevel>().Trigger();
    }
}