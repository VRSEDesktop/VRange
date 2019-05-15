using System;
using System.Collections;
using UnityEngine;
using Valve.VR;

public class Exercise : MonoBehaviour
{
	private ExerciseProgress _progress;
	public ExerciseProgress Progress
	{
		get { return _progress; }
		set
		{
			if (value != _progress)
			{
				_progress = value;
				OnProgressChanged();
			}
		}
	}
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
		Progress = ExerciseProgress.NotStarted;
		BulletLines.SetActive(Settings.DrawLines);
        Settings.SettingsChanged += OnSettingsChanged;
        foreach(ExcersiseState state in States) state.OnExit();
        CurrentState = 0;
    }

    public void Update()
    {      
        States[CurrentState].OnUpdate();
        HandleButtons();
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
		if(CurrentState != 2)
		{
			States[CurrentState].OnExit();
			CurrentState++;
			States[CurrentState].OnStart();
			DeleteBulletHoles();
			DeleteLines();
		}
    }

    public void Restart()
    {
		Progress = ExerciseProgress.NotStarted;
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
            ScenarioLogs.Clear();
            Restart();
            UI.DeactivateButton("Restart Scenario");
        }

        if (UI.GetButtonActivated("Mainmenu"))
        {
            ScenarioLogs.Clear();
            LevelLoader.levelName = "MainMenu";
            LevelLoader.Trigger();
        }

        if (UI.GetButtonActivated("Next Scenario"))
        {
            ScenarioLogs.Clear();
            NextStep();
            UI.DeactivateButton("Next Scenario");
        }

        if (UI.GetButtonActivated("Previous Scenario"))
        {
            ScenarioLogs.Clear();
            PreviousStep();
            UI.DeactivateButton("Previous Scenario");
        }

		if (UI.GetButtonActivated("Start Exercise"))
			StartCoroutine(StartTimer(3));
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

	private void OnProgressChanged()
	{
		States[CurrentState].OnProgressChanged();
	}

	/// <summary>
	/// Starts a timer before starting the exercise.
	/// </summary>
	/// <param name="seconds">The amount of seconds for the time.</param>
	/// <returns></returns>
	private IEnumerator StartTimer(int seconds)
	{
		float startTime = Time.time;
		float timeProgressed;
		while(true)
		{
			yield return null;
			timeProgressed = Time.time - startTime;
			int displayTime = (int)Mathf.Ceil(timeProgressed);

			if (timeProgressed >= seconds)
				break;
		}
		Progress = ExerciseProgress.Started;
	}
}