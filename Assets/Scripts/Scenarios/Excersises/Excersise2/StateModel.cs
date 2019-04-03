using System;
using System.Collections;
using UnityEngine;

public class StateModel : ExcersiseState
{
    public Animator anim;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float waitTime = 5f;


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
        System.Random random = new System.Random();

        waitTime = (float)random.Next(2, 5);

        switch(random.Next(1, 2))
        {
            case 1:
                StartCoroutine(PullGun());
                break;
            case 2:
                StartCoroutine(PullPhone());
                break;
        }      
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullGun()
    {
        yield return new WaitForSeconds(waitTime);
        anim.Play("Equip Pistol");
    }

    /// <summary>
    /// triggers the pullphone animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullPhone()
    {
        yield return new WaitForSeconds(waitTime);
        anim.Play("Equip Phone");
    }
}
