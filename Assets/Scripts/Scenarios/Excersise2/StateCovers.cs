using System.Collections;
using UnityEngine;

public class StateCovers : ExcersiseState
{
    public GameObject WomanPrefab;
    public Animator Anim;
    /// <summary>
    /// Minimum and maximum time in seconds after which the model will decide which item to take
    /// </summary>
    public float MinWaitTime = 2f, MaxWaitTime = 5f;

	public GameObject RespawnPoint;

    public override void OnInitialize()
    {
        base.OnInitialize();
        Exercise.PreviousScenarioButton.gameObject.SetActive(true);
        Exercise.NextScenarioButton.gameObject.SetActive(true);
    }

    public override void OnStart()
    {
        base.OnStart();
        Randomizer();
    }

    public override void OnExit()
    {
        base.OnExit();
        RespawnWoman();
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
        StartCoroutine(PullItem(waitTime));
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Anim.SetBool("Equip Pistol", true);
        yield return new WaitForSeconds(0.8f);
        Anim.GetComponent<Enemy>().Gun.gameObject.SetActive(true);
        Anim.GetComponent<Enemy>().isAgressive = true;
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
