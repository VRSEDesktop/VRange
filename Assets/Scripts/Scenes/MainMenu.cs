using UnityEngine;
using Valve.VR;

public class MainMenu : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;

    public void Update()
    {
        HandleButtons();
    }

    private void HandleButtons()
    {
        if (UI.GetButtonActivated("Exercise 2"))
        {
            ScenarioLogs.Clear();
            levelLoader.levelName = "Exercise_2_Scenario";
            levelLoader.Trigger();
        }
    }
}