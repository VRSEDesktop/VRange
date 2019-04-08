using UnityEngine;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] states;
    private static int currentState = 0;

    public delegate void ButtonDelegate();

    public event ButtonDelegate ButtonPressEvent;

    public void Start()
    {
        foreach(ExcersiseState state in states) state.OnExit();
        currentState = 0;
        states[currentState].OnStart();
    }

    public void Update()
    {      
        states[currentState].OnUpdate();
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
        Debug.Log("Restart " + states[currentState]);
        states[currentState].Restart();

    }

    public void FireButtonEvent()
    {
        ButtonPressEvent += FireButtonEvent;
    }
}