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
        if (Settings.DrawLines) UI.GetUIItem("Toggle Bulletlines").SetActive();

        Settings.SettingsChanged += OnSettingsChanged;
        foreach(ExcersiseState state in States) state.OnExit();
        CurrentState = 0;

        States[CurrentState].OnInitialize();
    }

    public void Update()
    {      
        States[CurrentState].OnUpdate();
        HandleButtons();
    }

    /// <summary>
    /// Cleares the scene and switches the scenario to the previous one from the list
    /// </summary>
    public void PreviousStep()
    {
        States[CurrentState].OnExit();
        CurrentState--;
        States[CurrentState].OnInitialize();

        DeleteBulletHoles();
        DeleteLines();
    }

    /// <summary>
    /// Cleares the scene and switches the scenario to the next one from the list
    /// </summary>
    public void NextStep()
    {  
		States[CurrentState].OnExit();
		CurrentState++;
		States[CurrentState].OnInitialize();

		DeleteBulletHoles();
		DeleteLines();
    }

    public void Restart()
    {
		Progress = ExerciseProgress.NotStarted;
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
            ScenarioLogs.Clear();
            Restart();
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
        }

        if (UI.GetButtonActivated("Previous Scenario"))
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

        if (UI.GetButtonActivated("Start Exercise"))
        {
            States[CurrentState].OnStart();
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