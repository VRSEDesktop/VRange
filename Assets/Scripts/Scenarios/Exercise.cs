using UnityEngine;
using Valve.VR;

public enum ExerciseProgress
{
	NotStarted,
	Started,
	Succeeded,
	Failed
}

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] States;
	public Settings Settings;
	public Player Player;

	public SteamVR_LoadLevel LevelLoader;
    public GazeButton PreviousScenarioButton, NextScenarioButton;
	public GazeButton StartButton, RestartButton;
	public GameObject ShootingRange, City;

	public Whiteboard whiteboard;

	/// <summary>
	/// Object with explanation of the exercise, reference used for turning it on/off
	/// </summary>
	public GameObject Explanation;

    public static int CurrentState;

	private ExerciseProgress _progress;
	public ExerciseProgress Progress
	{
		get { return _progress; }
		set
		{
			if (value != _progress)
			{
				_progress = value;
				States[CurrentState].OnProgressChanged();
			}
		}
	}

	public void Start()
    {
		BulletLines.SetActive(Settings.DrawLines);
        Settings.SettingsChanged += OnSettingsChanged;

		foreach (ExcersiseState state in States) state.gameObject.SetActive(false);

        States[CurrentState].OnInitialize();

		if (Settings.UseNormalGuns) GameObject.Find("Toggle Controller").GetComponent<GazeButtonToggle>().Activate();
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
        States[CurrentState].OnInitialize();

		Clear();
	}

	public void NextStep()
    {  
		States[CurrentState].OnExit();
		CurrentState++;
		States[CurrentState].OnInitialize();

		Clear();
	}


	public void Restart()
    {
		BulletLines.SetActive(Settings.DrawLines);

		Clear();

		States[CurrentState].Restart();
    }

	private void HandleButtons()
    {
        Settings.DrawLines = UI.GetButtonActivated("Toggle Bulletlines");
		Settings.UseNormalGuns = UI.GetButtonActivated("Toggle Controller");

		if (UI.GetButtonActivated("Restart Scenario")) Restart();

        if (UI.GetButtonActivated("Mainmenu"))
        {
            ScenarioLogs.Clear();

            LevelLoader.levelName = "MainMenu";
            LevelLoader.Trigger();
			CurrentState = 0;
		}

        if (UI.GetButtonActivatedAndTurnOff("Next Scenario")) NextStep();
        if (UI.GetButtonActivatedAndTurnOff("Previous Scenario")) PreviousStep();

		if (UI.GetButtonActivatedAndTurnOff("Start_Scenario"))
		{
			Clear();

			States[CurrentState].OnStart();
		}
	}

	private void OnSettingsChanged()
	{
		BulletLines.SetActive(Settings.DrawLines);

		ApplyGunRotation[] guns = GameObject.Find("[CameraRig]").GetComponentsInChildren<ApplyGunRotation>();
		foreach (ApplyGunRotation gun in guns) gun.Apply();
	}

	/// <summary>
	/// Clears the exercise from bullet lines, bullet holes and scenario logs
	/// </summary>
	public void Clear()
	{
		ScenarioLogs.Clear();
		BulletLines.Destroy();
		DeleteBulletHoles();
	}

	/// <summary>
	/// Deletes all game objects from folder-object with tag Bullet Hole 
	/// </summary>
	private void DeleteBulletHoles()
	{
		GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
		foreach (GameObject obj in bulletHoles) Destroy(obj);
	}
}