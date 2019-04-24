using UnityEngine;
using Valve.VR;

public class MainMenu : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;

    void Update()
    {
        HandleButtons();
    }

    private void HandleButtons()
    {
        if (UI.GetButtonActivated("Exercise 2"))
        {
            Scenario.Clear();
            levelLoader.levelName = "Exercise_2_Scenario";
            levelLoader.Trigger();
        }

        if (UI.GetButtonActivated("Shooting-range"))
        {
            Scenario.Clear();
            levelLoader.levelName = "ShootingRange";
            levelLoader.Trigger();
        }
    }
}