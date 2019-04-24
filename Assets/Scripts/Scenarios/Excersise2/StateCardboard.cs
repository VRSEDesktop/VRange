using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToStart = 5f;

    private static readonly float timeToReact = 2.5f;
    public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(TurningCardBoard());        
    }

    public override void OnUpdate()
    {
        UpdateGUI();
    }

    public override void OnExit()
    {
        base.OnExit();

        flipAnimation.SetBool("Visible", false);
    }

    private IEnumerator TurningCardBoard()
    {
        StartTime = Time.realtimeSinceStartup;

        for(int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(timeToStart);
            flipAnimation.SetBool("Visible", true);
            yield return new WaitForSeconds(timeToReact);
            flipAnimation.SetBool("Visible", false);
        }     
    }

    public override void Restart()
    {
        base.Restart();
        flipAnimation.SetBool("Visible", false);
        StartCoroutine(TurningCardBoard());
    }
}