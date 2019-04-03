using System;
using System.Collections;
using UnityEngine;

public class StateModel : ExcersiseState
{
    public Animator pullGunAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToPullGun = 5f;


    public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(PullGun());
    }

    public override void OnUpdate()
    {
        UpdateGUI();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private IEnumerator PullGun()
    {
        yield return new WaitForSeconds(timeToPullGun);
        pullGunAnimation.Play("Equip Pistol");
    }
}
