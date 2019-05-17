using System.Collections;
using UnityEngine;

public class StateStreet : ExcersiseState
{
	public GameObject WomanPrefab;
	public Animator WomanAnimator;
	public GameObject RespawnPoint;

	/// <summary>
	/// Minimum and maximum time in seconds after which the model will decide which item to take
	/// </summary>
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 5f;

	public override void OnStart()
    {
        base.OnStart();

		Exercise.ShootingRange.gameObject.GetComponent<Transition>().Disable();
		Exercise.City.gameObject.SetActive(true);
		Exercise.City.gameObject.GetComponent<Transition>().Enable();

		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject button in buttons) button.GetComponent<GazeButton>().SetState(false);

		Randomizer();
	}

	public override void OnExit()
	{
		base.OnExit();
		Exercise.City.gameObject.GetComponent<Transition>().Disable();
		Exercise.ShootingRange.gameObject.SetActive(true);
		Exercise.ShootingRange.gameObject.GetComponent<Transition>().Enable();
		RespawnWoman();
	}

	private void RespawnWoman()
	{
		Enemy woman = GetComponentInChildren<Enemy>();
		GameObject newWoman = Instantiate(WomanPrefab, RespawnPoint.transform.position, RespawnPoint.transform.rotation);
		newWoman.transform.parent = transform;
		Destroy(woman.gameObject);
		WomanAnimator = newWoman.GetComponent<Animator>();
	}

	private void Randomizer()
	{
		float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
		StartCoroutine(PullItem(waitTime, Random.Range(0, 1)));
	}

	/// <summary>
	/// Triggers the pullgun animation
	/// </summary>
	/// <returns></returns>
	private IEnumerator PullItem(float waitTime, int num)
	{
		yield return new WaitForSeconds(waitTime);
		switch (num)
		{
			case 0:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSeconds(0.8f);
				WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
				WomanAnimator.GetComponent<Enemy>().isAgressive = true;
				break;
		}

		yield return new WaitForSeconds(LoopTime);
		Restart();
	}

	public override void Restart()
	{
		base.Restart();

		RespawnWoman();
		Randomizer();
	}
}
