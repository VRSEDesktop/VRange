using System.Collections;
using UnityEngine;

public class StateModel : ExcersiseState
{
    public GameObject womanPrefab;
    public Animator anim;
    /// <summary>
    /// Minimum and maximum time in seconds after which the model will decide which item to take
    /// </summary>
    public float minWaitTime = 2f, maxWaitTime = 5f;

    private int currentAnimation = -1;

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
    }

    private void Randomizer()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        StartCoroutine(PullItem(waitTime, Random.Range(0, 1)));    
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(float waitTime, int num)
    {
        currentAnimation = num;

        yield return new WaitForSeconds(waitTime);      
        switch (num)
        {
            case 0: anim.SetBool("Equip Pistol", true); break; 
            case 1: anim.SetBool("Equip Phone", true); break;
        }      
    }

    public override void Restart()
    {
        base.Restart();

        Enemy woman = GetComponentInChildren<Enemy>();
        GameObject newWoman = Instantiate(womanPrefab, woman.transform.position, woman.transform.rotation);
        newWoman.transform.parent = transform;
        Destroy(woman.gameObject);
        anim = newWoman.GetComponent<Animator>();

        Randomizer();
    }
}
