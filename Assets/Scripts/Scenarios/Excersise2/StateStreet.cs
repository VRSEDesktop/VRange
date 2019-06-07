using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStreet : ExcersiseState
{
	public GameObject WomanPrefab;
	public Animator WomanAnimator;
	public GameObject RespawnPoint;
	public GameObject CurrentButtonSpawn;

	public GameObject player;

	public List<GameObject> PlayerSpawnPoints, SpawnPointsWoman;

	public List<Vector3> positions;
	public List<Quaternion> rotations;

	/// <summary>
	/// Minimum and maximum time in seconds after which the model will decide which item to take
	/// </summary>
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 15f;
	/// <summary>
	/// Time for animated human to reach their pocket for item
	/// </summary>
	private static readonly float TimeToReachPocket = 0.8f;
	/// <summary>
	/// Time for delaying part of the transition to make it more smoother
	/// </summary>
	private static readonly float TransitionDelay = 2f;


	public override void OnInitialize()
	{
		base.OnInitialize();

		StartCoroutine(DoTransition());
		OnStart();
	}

	public override void OnStart()
	{
		base.OnStart();

		Randomizer();
		Respawn(true);
	}

	private IEnumerator DoTransition()
	{
		Exercise.ShootingRange.gameObject.GetComponent<Transition>().Disable();
		yield return new WaitForSecondsRealtime(TransitionDelay);
		Exercise.City.gameObject.SetActive(true);
		Exercise.City.gameObject.GetComponent<Transition>().Enable();

		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		foreach (GameObject button in buttons) button.GetComponent<GazeButton>().SetState(false);
	}

	private IEnumerator UndoTransition()
	{
		Exercise?.City.gameObject.GetComponent<Transition>().Disable();
		yield return new WaitForSecondsRealtime(TransitionDelay);
		Exercise?.ShootingRange.gameObject.SetActive(true);
		Exercise?.ShootingRange.gameObject.GetComponent<Transition>().Enable();

		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		foreach (GameObject button in buttons) button.GetComponent<GazeButton>().SetState(true);
	}

	public override void OnExit()
	{
		base.OnExit();
		StartCoroutine(UndoTransition());
	}

	private void Randomizer()
	{
		float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
		StartCoroutine(PullItem(waitTime, Item.GUN));
	}

	/// <summary>
	/// Triggers the pullgun animation
	/// </summary>
	/// <returns></returns>
	private IEnumerator PullItem(float waitTime, Item item)
	{
		yield return new WaitForSeconds(waitTime);

		switch(item)
		{
			case Item.GUN:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSeconds(TimeToReachPocket);
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

		Respawn(false);
		Randomizer();
	}

	public void Respawn(bool _firstSpawn)
	{
		int spawnNum = 0;
		if (!_firstSpawn) spawnNum = Random.Range(0, PlayerSpawnPoints.Capacity);

		GameObject spawnPlayer = PlayerSpawnPoints[spawnNum];
		GameObject spawnWoman = SpawnPointsWoman[spawnNum];

		// Spawning woman
		Enemy woman = GetComponentInChildren<Enemy>();
		GameObject newWoman = Instantiate(WomanPrefab, spawnWoman.transform.position, spawnWoman.transform.rotation);
		newWoman.transform.parent = transform;
		if(woman) Destroy(woman.gameObject);
		WomanAnimator = newWoman.GetComponent<Animator>();

		// Spawning player
		if(!_firstSpawn) player.transform.SetPositionAndRotation(spawnPlayer.transform.position, spawnPlayer.transform.rotation);
	}
}
