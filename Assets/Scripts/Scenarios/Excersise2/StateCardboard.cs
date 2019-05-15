using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator FlipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float TimeToStart = 5f;
	private int Iteration;

	public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(TurningCardBoard(TimeToStart));

        Exercise.City.gameObject.SetActive(false);

        Exercise.PreviousScenarioButton.gameObject.SetActive(false);
        Exercise.NextScenarioButton.gameObject.SetActive(true);

		//FlipAnimation.gameObject.SetActive(true);
		Iteration = 0;
    }

    public override void OnExit()
    {
        base.OnExit();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 0;
		//FlipAnimation.gameObject.SetActive(false);
    }

    private IEnumerator TurningCardBoard(float _time)
    {
		if (Iteration <= 7)
		{
			yield return new WaitForSeconds(_time);
			FlipAnimation.SetBool("Visible", true);

			Iteration += 1;
		}
	}

    public override void Restart()
    {
        base.Restart();

        FlipAnimation.SetBool("Visible", false);
        StartCoroutine(TurningCardBoard(TimeToStart));
		Iteration = 0;
    }

	public void Hit()
	{
		FlipAnimation.SetBool("Visible", false);
		StartCoroutine(TurningCardBoard(1.5f));
	}


}