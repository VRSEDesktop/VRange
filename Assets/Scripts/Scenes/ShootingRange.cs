using UnityEngine;
using Valve.VR;

public class ShootingRange : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;

	public void Update()
    {
        HandleButtons();
    }

    private void HandleButtons()
    {
        if (UI.GetButtonActivated("MainMenu"))
        {
            ScenarioLogs.Clear();
            levelLoader.levelName = "MainMenu";
            levelLoader.Trigger();
        }
    }
}
