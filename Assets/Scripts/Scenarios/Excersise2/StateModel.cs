using System.Collections;
using UnityEngine;

public class StateModel : ExcersiseState
{
    public GameObject WomanPrefab;
    public Animator WomanAnimator;
	public GameObject RespawnPoint;
	private IEnumerator CurrentCoroutine;

	/// <summary>
	/// Minimum and maximum time in seconds after which the model will decide which item to take
	/// </summary>
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 6f;

	private enum Item
	{
		GUN, PHONE
	}

	public override void OnInitialize()
	{
		base.OnInitialize();

		Exercise.PreviousScenarioButton.SetState(true);
		Exercise.NextScenarioButton.SetState(true);
	}

	public override void OnStart()
    {
        base.OnStart();

		if (WomanAnimator == null || WomanAnimator.GetComponent<Enemy>().IsDead) Restart();
		Randomizer();
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

		if(CurrentCoroutine != null) StopCoroutine(CurrentCoroutine);

		// 80% for gun, 20% phone
		Item item = Random.Range(0, 100) < 80 ? Item.GUN : Item.PHONE;

		CurrentCoroutine = PullItem(waitTime, item);
		if (isActiveAndEnabled) StartCoroutine(CurrentCoroutine);    
    }

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (WomanAnimator == null || WomanAnimator.GetComponent<Enemy>().IsDead)
		{
			Exercise.Progress = ExerciseProgress.Succeeded;
			//Restart();
		}
	}

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime, Item item)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        switch(item)
        {
            case Item.GUN:
		        WomanAnimator.SetBool("Equip Pistol", true);
                yield return new WaitForSecondsRealtime(0.8f);
                WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
                WomanAnimator.GetComponent<Enemy>().isAgressive = true;
            break;

            case Item.PHONE:
		        WomanAnimator.SetBool("Equip Phone", true);
                yield return new WaitForSecondsRealtime(0.8f);
                WomanAnimator.GetComponent<Enemy>().Phone.gameObject.SetActive(true);
                WomanAnimator.GetComponent<Enemy>().isAgressive = false;
            break;
		}

        yield return new WaitForSecondsRealtime(LoopTime);
        Restart();
    }

    public override void Restart()
    {
        base.Restart();

        RespawnWoman();
        Randomizer();

		BulletLines.SetActive(Exercise.Settings.DrawLines);
	}

    private void RespawnWoman()
    {
        Enemy woman = GetComponentInChildren<Enemy>();
        GameObject newWoman = Instantiate(WomanPrefab, RespawnPoint.transform.position, RespawnPoint.transform.rotation);
        newWoman.transform.parent = transform;
        if(woman) Destroy(woman.gameObject);
        WomanAnimator = newWoman.GetComponent<Animator>();
    }
}
