using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animator FlipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float TimeToStart = 5f, ReapearTime = 2f, TimeToReact = 2f;
	private int Iteration = 1;

	private const int Repetitions = 7;

    private IEnumerator CurrentCoroutine;

	public override void OnInitialize()
	{
		base.OnInitialize();

		Exercise.City.gameObject.SetActive(false);

		if (Exercise.PreviousScenarioButton != null) Exercise.PreviousScenarioButton.SetState(false);
		Exercise.NextScenarioButton.SetState(true);
	}

	public override void OnStart()
    {
		base.OnStart();

        RestartCourutine(TimeToStart);
    }

    public override void OnExit()
    {
        base.OnExit();

		if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
		FlipAnimation.SetBool("Visible", false);
		Iteration = 1;
    }

    public override void Restart()
    {
        base.Restart();

        FlipAnimation.SetBool("Visible", false);
		Iteration = 1;

        RestartCourutine(TimeToStart);
    }
	public override void OnFinish()
	{
		base.OnFinish();

		if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
		StartCoroutine(ShowResults());
	}

	private IEnumerator ShowResults()
	{
		yield return new WaitForSecondsRealtime(1f);
		FlipAnimation.SetBool("Visible", true);
	}

	private IEnumerator TurningCardBoard(float _time)
    {
		yield return new WaitForSecondsRealtime(_time);
		FlipAnimation.SetBool("Visible", true);
		yield return new WaitForSecondsRealtime(TimeToReact);

		if (Iteration < Repetitions)
        {          
            FlipAnimation.SetBool("Visible", false);
			Iteration++;

			RestartCourutine(ReapearTime);
        }
        else if (Iteration == Repetitions) // On last repetition dont restart and finish step
        {
			Exercise.Progress = ExerciseProgress.Succeeded;
        }
    }

    /// <summary>
    /// Called when the cardboard was hit. Called using method call by name from whiteboard
    /// </summary>
	public void OnHit()
	{
		Debug.Log("OnHit() " + Exercise.Progress);
		if (Exercise.Progress == ExerciseProgress.NotStarted) return;
		
		FlipAnimation.SetBool("Visible", false);
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
        if(isActiveAndEnabled) StartCoroutine(CurrentCoroutine);
    }
}