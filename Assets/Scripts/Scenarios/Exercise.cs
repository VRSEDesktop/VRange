﻿using UnityEngine;
using Valve.VR;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] States;
    public Settings Settings;
    public SteamVR_LoadLevel LevelLoader;
    public GazeButton PreviousScenarioButton, NextScenarioButton;
	public GameObject ShootingRange, City;

	/// <summary>
	/// Object with explanation of the exercise, reference used for turning it on/off
	/// </summary>
	public GameObject Explanation;

    private static int CurrentState = 0;

    public void Start()
    {
        foreach(ExcersiseState state in States) state.OnExit();
        CurrentState = 0;
        States[CurrentState].OnStart();
    }

    public void Update()
    {      
        States[CurrentState].OnUpdate();
        HandleButtons();
        if(BulletLine.Parent) BulletLine.Parent.SetActive(Settings.DrawLines);
    }

    public void PreviousStep()
    {
        States[CurrentState].OnExit();
        CurrentState--;
        States[CurrentState].OnStart();

        DeleteBulletHoles();
        DeleteLines();
    }

    public void NextStep()
    {
        States[CurrentState].OnExit();
        CurrentState++;
        States[CurrentState].OnStart();

        DeleteBulletHoles();
        DeleteLines();
    }

    public void Restart()
    {
        DeleteBulletHoles();
        DeleteLines();
        States[CurrentState].Restart();
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

		if (UI.GetButtonActivated("Toggle Controller"))
		{
			Settings.NormalGun = !Settings.NormalGun;

			ApplyGunRotation[] guns = GameObject.Find("[CameraRig]").GetComponentsInChildren<ApplyGunRotation>();
			foreach (ApplyGunRotation gun in guns) gun.Apply();
		}
	}

    private void DeleteBulletHoles()
    {
        GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
        foreach (GameObject obj in bulletHoles) Destroy(obj);
    }

    private void DeleteLines()
    {
        GameObject shotsContainer = GameObject.Find("ShotsRays");
        Destroy(shotsContainer);
    }
}