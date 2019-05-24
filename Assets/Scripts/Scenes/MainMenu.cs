using UnityEngine;
using Valve.VR;

public class MainMenu : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;
	public Settings Settings;

	public void Start()
	{
		if (Settings.NormalGun) GameObject.Find("Toggle Controller").GetComponent<GazeButtonToggle>().Activate();
	}

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

		if (Settings.NormalGun != UI.GetButtonActivated("Toggle Controller"))
		{
			ApplyGunRotation[] guns = GameObject.Find("[CameraRig]").GetComponentsInChildren<ApplyGunRotation>();
			foreach (ApplyGunRotation gun in guns) gun.Toggle();
			Settings.NormalGun = !Settings.NormalGun;
		}

		if (UI.GetButtonActivated("Exit Game"))
        {
            Application.Quit();
        }
    }
}