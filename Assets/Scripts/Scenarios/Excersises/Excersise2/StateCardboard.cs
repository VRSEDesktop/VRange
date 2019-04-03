using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animation flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToFlipCardboard = 5f;

    

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
    }

    private IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(timeToFlipCardboard);
        flipAnimation.Play();
    }

    
}
