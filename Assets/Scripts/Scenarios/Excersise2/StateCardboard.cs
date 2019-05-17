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
	private bool WasHit = false;

	private const int Repetitions = 7;

	public override void OnStart()
    {
		base.OnStart();
        StartCoroutine(TurningCardBoard(TimeToStart));

        Exercise.City.gameObject.SetActive(false);

		if(Exercise.PreviousScenarioButton != null)Exercise.PreviousScenarioButton.SetState(false);
        Exercise.NextScenarioButton.SetState(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;
    }

    private IEnumerator TurningCardBoard(float _time)
    {
		if (Iteration < Repetitions)
		{
			yield return new WaitForSecondsRealtime(_time);
			FlipAnimation.SetBool("Visible", true);

			Iteration++;

			yield return new WaitForSecondsRealtime(TimeToReact);

			if (!WasHit)
			{				
				if(Iteration != Repetitions) FlipAnimation.SetBool("Visible", false);
				StartCoroutine(TurningCardBoard(ReapearTime));
			}
		}

		if (Iteration == Repetitions)
		{
			yield return new WaitForSecondsRealtime(TimeToReact);
			Progress = ExerciseProgress.Succeeded;
		}
	}

    public override void Restart()
    {
        base.Restart();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;
		WasHit = false;
		StartCoroutine(TurningCardBoard(TimeToStart));
	}

	public void Hit()
	{
		if(Iteration != Repetitions) FlipAnimation.SetBool("Visible", false);
		WasHit = true;
		StartCoroutine(TurningCardBoard(ReapearTime));
	}
}