using UnityEngine;

public class Exercise : MonoBehaviour
{
    /// <summary>
    /// States of the excersises
    /// </summary>
    public static ExcersiseState[] states;
    private static int currentState = 0;

    public void Update()
    {      
        states[currentState].OnUpdate();
    }

    public static void PreviousStep()
    {
        states[currentState].OnExit();
        currentState--;
        states[currentState].OnStart();
    }

    public static void NextStep()
    {
        states[currentState].OnExit();
        currentState++;
        states[currentState].OnStart();
    }
}
