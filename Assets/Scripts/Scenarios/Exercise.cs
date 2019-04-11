using UnityEngine;

[RequireComponent(typeof(Settings))]
public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] states;
    public Settings Settings;
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
    }

    public void NextStep()
    {
        states[currentState].OnExit();
        currentState++;
        states[currentState].OnStart();
    }

    public void Restart()
    {
        states[currentState].Restart();
    }

    private void HandleButtons()
    {
        Settings.drawLines = UI.GetButtonActivated("Toggle Bulletlines");
    }
}