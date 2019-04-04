using UnityEngine;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public ExcersiseState[] states;
    private static int currentState = 0;

    public void Start()
    {
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
        states[currentState].Restart();
    }
}