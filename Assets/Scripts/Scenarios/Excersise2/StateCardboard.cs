using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator FlipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float TimeToStart = 5f, ReapearTime = 2f, TimeToReact = 3f;
	private int Iteration;

	private const int Repetitions = 7;

    private IEnumerator CurrentCoroutine;

	public override void OnStart()
    {
		base.OnStart();
        RestartCourutine(TimeToStart);

        Exercise.City.gameObject.SetActive(false);

		Exercise.PreviousScenarioButton.SetState(false);
        Exercise.NextScenarioButton.SetState(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;
    }

    public override void Restart()
    {
        base.Restart();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;

        RestartCourutine(TimeToStart);
    }

    private IEnumerator TurningCardBoard(float _time)
    {
        if (Iteration < Repetitions)
        {
            yield return new WaitForSecondsRealtime(_time);
            FlipAnimation.SetBool("Visible", true);

            Iteration++;

            yield return new WaitForSecondsRealtime(TimeToReact);

            FlipAnimation.SetBool("Visible", false);

			RestartCourutine(ReapearTime);
        }
        else if (Iteration == Repetitions)
        {
            yield return new WaitForSecondsRealtime(TimeToReact);
            Progress = ExerciseProgress.Succeeded;
        }
    }

    /// <summary>
    /// Called when the cardboard was hit
    /// </summary>
	public void OnHit()
	{
		if(Iteration != Repetitions) FlipAnimation.SetBool("Visible", false);
        RestartCourutine(ReapearTime);
	}

    /// <summary>
    /// Stops the current coroutine, if it's running, and starts the new one
    /// </summary>
    /// <param name="_time"></param>
    private void RestartCourutine(float _time)
    {
        if(CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
        CurrentCoroutine = TurningCardBoard(_time);
        StartCoroutine(CurrentCoroutine);
    }
}