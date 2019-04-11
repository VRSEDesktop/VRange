using System.Collections;
using UnityEngine;

public class StateCovers : ExcersiseState
{
    public GameObject womanPrefab;
    public Animator anim;
    /// <summary>
    /// Minimum and maximum time in seconds after which the model will decide which item to take
    /// </summary>
    public float minWaitTime = 2f, maxWaitTime = 5f;

    public override void OnStart()
    {
        base.OnStart();
        Randomizer();
    }

    public override void OnUpdate()
    {
        UpdateGUI();
    }

    public override void OnExit()
    {
        base.OnExit();
        RespawnWoman();
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        StartCoroutine(PullItem(waitTime));
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        anim.SetBool("Equip Pistol", true);
        yield return new WaitForSeconds(0.8f);
        anim.GetComponent<Enemy>().gun.gameObject.SetActive(true);
        anim.GetComponent<Enemy>().isAgressive = true;
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
        GameObject newWoman = Instantiate(womanPrefab, woman.transform.position, woman.transform.rotation);
        newWoman.transform.parent = transform;
        Destroy(woman.gameObject);
        anim = newWoman.GetComponent<Animator>();
    }
}
