using System.Collections;
using UnityEngine;

public class StateModel : ExcersiseState
{
    public GameObject WomanPrefab;
    public Animator WomanAnimator;
	public GameObject RespawnPoint;

	/// <summary>
	/// Minimum and maximum time in seconds after which the model will decide which item to take
	/// </summary>
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 6f;

	public override void OnStart()
    {
        base.OnStart();
        Randomizer();

        Exercise.PreviousScenarioButton.SetState(true);
        Exercise.NextScenarioButton.SetState(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        RespawnWoman();
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
        StartCoroutine(PullItem(waitTime, Random.Range(0, 5)));    
    }

	public override void OnUpdate()
	{
		base.OnUpdate();

		if(WomanAnimator.GetComponent<Enemy>().IsDead) Progress = ExerciseProgress.Succeeded;
	}

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime, int num)
    {
        yield return new WaitForSecondsRealtime(waitTime);      
        switch (num)
        {
            case 0:
		        WomanAnimator.SetBool("Equip Pistol", true);
                yield return new WaitForSecondsRealtime(0.8f);
                WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
                WomanAnimator.GetComponent<Enemy>().isAgressive = true;
            break; 
            case 1:
		        WomanAnimator.SetBool("Equip Phone", true);
                yield return new WaitForSecondsRealtime(0.8f);
                WomanAnimator.GetComponent<Enemy>().Phone.gameObject.SetActive(true);
                WomanAnimator.GetComponent<Enemy>().isAgressive = false;
                break;
			case 2:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSecondsRealtime(0.8f);
				WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
				WomanAnimator.GetComponent<Enemy>().isAgressive = true;
				break;
			case 3:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSecondsRealtime(0.8f);
				WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
				WomanAnimator.GetComponent<Enemy>().isAgressive = true;
				break;
			case 4:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSecondsRealtime(0.8f);
				WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
				WomanAnimator.GetComponent<Enemy>().isAgressive = true;
				break;

			case 5:
				WomanAnimator.SetBool("Equip Pistol", true);
				yield return new WaitForSecondsRealtime(0.8f);
				WomanAnimator.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
				WomanAnimator.GetComponent<Enemy>().isAgressive = true;
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
    }

    private void RespawnWoman()
    {
        Enemy woman = GetComponentInChildren<Enemy>();
        GameObject newWoman = Instantiate(WomanPrefab, RespawnPoint.transform.position, RespawnPoint.transform.rotation);
        newWoman.transform.parent = transform;
        Destroy(woman.gameObject);
        WomanAnimator = newWoman.GetComponent<Animator>();
    }
}
