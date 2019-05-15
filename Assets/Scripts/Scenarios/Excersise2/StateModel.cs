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
	public float MinWaitTime = 2f, MaxWaitTime = 5f, LoopTime = 5f;

	public override void OnStart()
    {
        base.OnStart();
        Randomizer();

        Exercise.PreviousScenarioButton.gameObject.SetActive(true);
        Exercise.NextScenarioButton.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        RespawnWoman();
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
        StartCoroutine(PullItem(waitTime, Random.Range(0, 2)));    
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
            case 1:
		        WomanAnimator.SetBool("Equip Phone", true);
                yield return new WaitForSeconds(0.8f);
                WomanAnimator.GetComponent<Enemy>().Phone.gameObject.SetActive(true);
                WomanAnimator.GetComponent<Enemy>().isAgressive = false;
                break;
            //case 2:
            //  Anim.SetBool("Equip Pistol", true);
            //    yield return new WaitForSeconds(0.8f);
            //    Anim.GetComponent<Enemy>().BaseballBat.gameObject.SetActive(true);
            //    Anim.GetComponent<Enemy>().isAgressive = true;
            //    break;
            //case 3:
            //    Anim.SetBool("Equip Pistol", true);
            //    yield return new WaitForSeconds(0.8f);
            //    Anim.GetComponent<Enemy>().Axe.gameObject.SetActive(true);
            //    Anim.GetComponent<Enemy>().isAgressive = true;
            //    break;
        }

        //yield return new WaitForSeconds(LoopTime);
        //Restart();
    }

    public override void Restart()
    {
		Debug.Log(ScenarioLogs.GetHits().Count);
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
