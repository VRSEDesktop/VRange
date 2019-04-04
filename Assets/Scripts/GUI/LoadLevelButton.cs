using TMPro;
using UnityEngine;
using Valve.VR;

public class LoadLevelButton : MonoBehaviour, ISightActivable
{
    public SteamVR_LoadLevel levelLoader;

    public string text;
    public string levelName;

    public void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = text;
    }

    public void Activate()
    {
        Scenario.Clear();

        levelLoader.levelName = levelName;
        levelLoader.Trigger();
    }

    public void OnHoverStart()
    {
        GetComponentInChildren<Animator>().Play("");
    }

    public void OnHoverEnd()
    {
        GetComponentInChildren<Animator>();
    }
}