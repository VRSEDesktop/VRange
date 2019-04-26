using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator FlipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float TimeToStart = 5f;

    private static readonly float TimeToReact = 2.5f;

	public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(TurningCardBoard());

        Exercise.City.gameObject.SetActive(false);

        Exercise.PreviousScenarioButton.gameObject.SetActive(false);
        Exercise.NextScenarioButton.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();

        FlipAnimation.SetBool("Visible", false);
    }

    private IEnumerator TurningCardBoard()
    {
        StartTime = Time.realtimeSinceStartup;

		for (int i = 0; i < 7; i++)
		{
			yield return new WaitForSeconds(TimeToStart);
			FlipAnimation.SetBool("Visible", true);
			yield return new WaitForSeconds(TimeToReact);
			FlipAnimation.SetBool("Visible", false);
		}      
    }

    public override void Restart()
    {
        base.Restart();
        FlipAnimation.SetBool("Visible", false);
        StartCoroutine(TurningCardBoard());
    }
}