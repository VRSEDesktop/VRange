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

	public override void OnStart()
    {
		base.OnStart();
        StartCoroutine(TurningCardBoard(TimeToStart));

        Exercise.City.gameObject.SetActive(false);

        Exercise.PreviousScenarioButton.gameObject.SetActive(false);
        Exercise.NextScenarioButton.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;
    }

    private IEnumerator TurningCardBoard(float _time)
    {
		if (Iteration < 7)
		{
			yield return new WaitForSeconds(_time);
			FlipAnimation.SetBool("Visible", true);

			Iteration++;

			yield return new WaitForSeconds(TimeToReact);

			if (!WasHit)
			{				
				FlipAnimation.SetBool("Visible", false);
				StartCoroutine(TurningCardBoard(ReapearTime));
			}
		}

		if (Iteration == 7)
		{
			yield return new WaitForSeconds(TimeToReact);
			Exercise.Progress = ExerciseProgress.Succeeded;
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
		if(Iteration != 8) FlipAnimation.SetBool("Visible", false);
		WasHit = true;
		StartCoroutine(TurningCardBoard(ReapearTime));
	}
}