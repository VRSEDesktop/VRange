using System.Collections;
using UnityEngine;

public class StateCovers : ExcersiseState
{
	private IEnumerator CurrentCoroutine;

	public GameObject WomanPrefab;
    public Animator Anim;
    /// <summary>
    /// Minimum and maximum time in seconds after which the model will decide which item to take
    /// </summary>
    public float MinWaitTime = 2f, MaxWaitTime = 5f, TimeForReaction = 6f;

	public GameObject RespawnPoint;

	public override void OnInitialize()
	{
		base.OnInitialize();

		Exercise.PreviousScenarioButton.SetState(true);
		Exercise.NextScenarioButton.SetState(true);
	}

	public override void OnStart()
    {
        base.OnStart();

        Randomizer();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
		if (Anim.GetComponent<Enemy>().IsDead) Exercise.Progress = ExerciseProgress.Succeeded;
	}

    public override void OnExit()
    {
        base.OnExit();

		if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
		RespawnWoman();
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(MinWaitTime, MaxWaitTime);

		if (CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);
		CurrentCoroutine = PullItem(waitTime);
		if (isActiveAndEnabled) StartCoroutine(CurrentCoroutine);
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        Anim.SetBool("Equip Pistol", true);
        yield return new WaitForSecondsRealtime(0.8f);
        Anim.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
        Anim.GetComponent<Enemy>().isAgressive = true;

		yield return new WaitForSecondsRealtime(TimeForReaction);
		Restart();
	}

    public override void Restart()
    {
        base.Restart();

        RespawnWoman();
        Randomizer();
    }

    private void RespawnWoman()
    {
        Enemy woman = GetComponentInChildren<Enemy>();
        GameObject newWoman = Instantiate(WomanPrefab, RespawnPoint.transform.position, RespawnPoint.transform.rotation);
        newWoman.transform.parent = transform;
        Destroy(woman.gameObject);
        Anim = newWoman.GetComponent<Animator>();
    }
}
