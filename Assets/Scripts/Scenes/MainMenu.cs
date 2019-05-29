using UnityEngine;
using Valve.VR;

public class MainMenu : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;
	public Settings Settings;

	public void Start()
	{
		Settings.SettingsChanged += OnSettingsChanged;
		if (Settings.UseNormalGuns) GameObject.Find("Toggle Controller").GetComponent<GazeButtonToggle>().Activate();
	}

	public void Update()
    {
        HandleButtons();
	}

    private void HandleButtons()
    {
        if (UI.GetButtonActivated("Exercise_2"))
        {
			ScenarioLogs.Clear();
			levelLoader.levelName = "Exercise_2_Scenario";
			levelLoader.Trigger();
		}

		Settings.UseNormalGuns = UI.GetButtonActivated("Toggle Controller");

		if (UI.GetButtonActivated("Exit Game")) Application.Quit();
    }

	private void OnSettingsChanged()
	{
		ApplyGunRotation[] guns = GameObject.Find("[CameraRig]").GetComponentsInChildren<ApplyGunRotation>();
		foreach (ApplyGunRotation gun in guns) gun.Apply();
	}
}