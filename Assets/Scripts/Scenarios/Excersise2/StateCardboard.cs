using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToStart = 5f;

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
        yield return new WaitForSeconds(timeToStart);

        startTime = Time.realtimeSinceStartup;
        flipAnimation.SetBool("Visible", true);
    }

    public override void Restart()
    {
        base.Restart();
        flipAnimation.SetBool("Visible", false);
        StartCoroutine(TurningCardBoard());
    }
}