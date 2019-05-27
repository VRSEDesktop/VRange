using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MainMenu : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;
	public Settings Settings;
	private string debug;

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
        if (UI.GetButtonActivated("Exercise_2"))
        {
			//ScenarioLogs.Clear();
			debug = "1";

			levelLoader.levelName = "Exercise_2_Scenario";
			debug = "2";
			levelLoader.Trigger();
			debug = "3";
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

	public void OnGUI()
	{
		for (int i = 0; i < 1; i++)
		{
			GUI.Label(new Rect(Screen.width / 12, Screen.height / 24 * i, Screen.width / 4 * 2, Screen.height / 6), debug);
		}
	}
}