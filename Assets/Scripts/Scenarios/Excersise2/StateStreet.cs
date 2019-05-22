﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStreet : ExcersiseState
{
	public GameObject WomanPrefab;
	public Animator WomanAnimator;
	public GameObject RespawnPoint;
	public GameObject player;

	public List<GameObject> SpawnPoints;
	public List<GameObject> SpawnPointsWoman;

	/// <summary>
	/// Minimum and maximum time in seconds after which the model will decide which item to take
	/// </summary>
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 8f;

	public override void OnInitialize()
	{
		base.OnInitialize();

		Exercise.ShootingRange.gameObject.SetActive(false);
		Exercise.City.gameObject.SetActive(true);

		//GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		//foreach (GameObject button in buttons) button.GetComponent<GazeButton>().SetState(false);
	}

	public override void OnStart()
	{
		base.OnStart();

		StartCoroutine(DoTransition());

		//GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		//foreach (GameObject button in buttons) button.GetComponent<GazeButton>().SetState(false);

		Randomizer();
		Respawn();
		
	}

	private IEnumerator DoTransition()
	{
		yield return new WaitForSecondsRealtime(2);
		Exercise.City.gameObject.SetActive(true);
	}

	private IEnumerator UndoTransition()
	{
		yield return new WaitForSecondsRealtime(2);
		Exercise?.ShootingRange.gameObject.SetActive(true);
	}

	public override void OnExit()
	{
		base.OnExit();
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
	}

	public override void Restart()
	{
		base.Restart();

		Respawn();
		Randomizer();
	}

	public void Respawn()
	{
		int spawnNum = Random.Range(0, 4);

		GameObject spawnPlayer = SpawnPoints[spawnNum];
		GameObject spawnWoman = SpawnPointsWoman[spawnNum];

		// Spawning woman
		Enemy woman = GetComponentInChildren<Enemy>();
		GameObject newWoman = Instantiate(WomanPrefab, spawnWoman.transform.position, spawnWoman.transform.rotation);
		newWoman.transform.parent = transform;
		Destroy(woman.gameObject);
		WomanAnimator = newWoman.GetComponent<Animator>();

		// Spawning player
		player.transform.SetPositionAndRotation(spawnPlayer.transform.position, spawnPlayer.transform.rotation);
	}
}
