using TMPro;
using UnityEngine;
using Valve.VR;

public class LoadLevelButton : Button
{
    public SteamVR_LoadLevel levelLoader;

    public string text;
    public string levelName;

    public override void Start()
    {
        base.Start();

        GetComponentInChildren<TextMeshPro>().text = text;
    }

    public override void Activate()
    {
        Scenario.Clear();

        levelLoader.levelName = levelName;
        levelLoader.Trigger();
    }
}