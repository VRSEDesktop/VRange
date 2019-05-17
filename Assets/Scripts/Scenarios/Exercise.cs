﻿using System.Collections;
using UnityEngine;
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

	public Whiteboard whiteboard;

	/// <summary>
	/// Object with explanation of the exercise, reference used for turning it on/off
	/// </summary>
	public GameObject Explanation;

    public static int CurrentState = 0;

	private bool Active = true;


	public void Start()
    {
		BulletLines.SetActive(Settings.DrawLines);
        Settings.SettingsChanged += OnSettingsChanged;
        foreach(ExcersiseState state in States) state.OnExit();
        CurrentState = 0;
        States[CurrentState].OnStart();
    }

	public void Update()
    {      
        States[CurrentState].OnUpdate();
        HandleButtons();
    }

	public void PreviousStep()
    {
        States[CurrentState].OnExit();
		CurrentState -= 1;
        States[CurrentState].OnStart();

        DeleteBulletHoles();
        DeleteLines();
    }

	public void OnStart()
	{
		BulletLines.SetActive(Settings.DrawLines);
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
		Debug.Log("Exercise:Restart()");
		BulletLines.SetActive(Settings.DrawLines);
		DeleteBulletHoles();
        DeleteLines();
        States[CurrentState].Restart();
    }

	private void HandleButtons()
    {
        Settings.DrawLines = UI.GetButtonActivated("Toggle Bulletlines");

        if (UI.GetButtonActivated("Restart Scenario"))
        {
			Debug.Log("Restart Scenario");
            ScenarioLogs.Clear();
            Restart();
        }

        if (UI.GetButtonActivated("Mainmenu"))
        {
            ScenarioLogs.Clear();
            LevelLoader.levelName = "MainMenu";
            LevelLoader.Trigger();
        }

        if (UI.GetButtonActivatedAndTurnOff("Next Scenario"))
        {
            ScenarioLogs.Clear();
            NextStep();
        }

        if (UI.GetButtonActivatedAndTurnOff("Previous Scenario"))
        {
			ScenarioLogs.Clear();
			PreviousStep();
        }

		if (UI.GetButtonActivated("Toggle Controller"))
		{
			ApplyGunRotation[] guns = GameObject.Find("[CameraRig]").GetComponentsInChildren<ApplyGunRotation>();
			foreach (ApplyGunRotation gun in guns) gun.Toggle();
			Settings.NormalGun = !Settings.NormalGun;
		}
	}

	private void OnSettingsChanged()
	{
		BulletLines.SetActive(Settings.DrawLines);
	}

    public void DeleteBulletHoles()
    {
        GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
        foreach (GameObject obj in bulletHoles) Destroy(obj);
    }

	public void DeleteLines()
    {
        BulletLines.Destroy();
    }

	private IEnumerator WaitForPermision(bool target)
	{
		yield return new WaitForSeconds(1f);
		target = true;
	}
}