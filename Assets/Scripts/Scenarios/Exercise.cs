using UnityEngine;
using Valve.VR;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] states;
    public Settings Settings;
    public SteamVR_LoadLevel levelLoader;
    public GazeButton PreviousScenarioButton, NextScenarioButton;
    public GameObject ShootingRange, City;

    private static int currentState = 0;

    public void Start()
    {
        foreach(ExcersiseState state in states) state.OnExit();
        currentState = 0;
        states[currentState].OnStart();
    }

    public void Update()
    {      
        states[currentState].OnUpdate();
        HandleButtons();
    }

    public void PreviousStep()
    {
        states[currentState].OnExit();
        currentState--;
        states[currentState].OnStart();

        DeleteBulletHoles();
    }

    public void NextStep()
    {
        states[currentState].OnExit();
        currentState++;
        states[currentState].OnStart();

        DeleteBulletHoles();
    }

    public void Restart()
    {
        DeleteBulletHoles();
        states[currentState].Restart();
    }

    private void HandleButtons()
    {
        Settings.drawLines = UI.GetButtonActivated("Toggle Bulletlines");

        if (UI.GetButtonActivated("Restart Scenario"))
        {
            Scenario.Clear();
            Restart();
            UI.DeactivateButton("Restart Scenario");
        }

        if (UI.GetButtonActivated("Mainmenu"))
        {
            Scenario.Clear();
            levelLoader.levelName = "MainMenu";
            levelLoader.Trigger();
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

    private void DeleteBulletHoles()
    {
        GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
        foreach (GameObject obj in bulletHoles) Destroy(obj);
    }
}