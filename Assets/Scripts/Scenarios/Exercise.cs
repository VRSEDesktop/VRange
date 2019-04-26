using System;
using UnityEngine;
using Valve.VR;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] States;
	private Settings _settings;
    public Settings Settings {
		get {
			return _settings;
		}
		set {
			_settings = value;
			OnSettingsChanged();
		}
	}

	

	public SteamVR_LoadLevel LevelLoader;
    public GazeButton PreviousScenarioButton, NextScenarioButton;
    public GameObject ShootingRange, City;

    private static int currentState = 0;

    public void Start()
    {
        foreach(ExcersiseState state in States) state.OnExit();
        currentState = 0;
        States[currentState].OnStart();
    }

    public void Update()
    {      
        States[currentState].OnUpdate();
        HandleButtons();
    }

    public void PreviousStep()
    {
        States[currentState].OnExit();
        currentState--;
        States[currentState].OnStart();

        DeleteBulletHoles();
        DeleteLines();
    }

    public void NextStep()
    {
        States[currentState].OnExit();
        currentState++;
        States[currentState].OnStart();

        DeleteBulletHoles();
        DeleteLines();
    }

    public void Restart()
    {
        DeleteBulletHoles();
        DeleteLines();
        States[currentState].Restart();
    }

    private void HandleButtons()
    {
        Settings.DrawLines = UI.GetButtonActivated("Toggle Bulletlines");

        if (UI.GetButtonActivated("Restart Scenario"))
        {
            Scenario.Clear();
            Restart();
            UI.DeactivateButton("Restart Scenario");
        }

        if (UI.GetButtonActivated("Mainmenu"))
        {
            Scenario.Clear();
            LevelLoader.levelName = "MainMenu";
            LevelLoader.Trigger();
        }

        if (UI.GetButtonActivated("Next Scenario"))
        {
            Scenario.Clear();
            NextStep();
            UI.DeactivateButton("Next Scenario");
        }

        if (UI.GetButtonActivated("Previous Scenario"))
        {
            Scenario.Clear();
            PreviousStep();
            UI.DeactivateButton("Previous Scenario");
        }
    }

	private void OnSettingsChanged()
	{
		BulletLines.SetActive(Settings.DrawLines);
	}

    private void DeleteBulletHoles()
    {
        GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
        foreach (GameObject obj in bulletHoles) Destroy(obj);
    }

    private void DeleteLines()
    {
        BulletLines.Destroy();
    }
}