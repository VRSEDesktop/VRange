using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animation flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToStart = 5f;

    private float maxTime = 15f;

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
        yield return new WaitForSeconds(timeToStart);
        flipAnimation.Play();
    }

    public override void Restart()
    {
        flipAnimation.transform.rotation = new Quaternion(0, -90, 0, 0);

        StartCoroutine(TurningCardBoard());
    }

    
}
