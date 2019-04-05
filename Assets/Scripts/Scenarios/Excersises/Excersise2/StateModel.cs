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

        waitTime = random.Next(2, 5);

        PullItem(random.Next(1, 2));    
    }

    /// <summary>
    /// Triggers the pullgun animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullItem(int num)
    {
        yield return new WaitForSeconds(waitTime);
        switch (num)
        {
            case 1:
                anim.Play("Equip Pistol");
                break;
            case 2:
                anim.Play("Equip Phone");
                break;
            case 3:
                anim.Play("Equip Phone");
                break;
        }
        
    }

    public override void Restart()
    {
        base.Restart();

        Randomizer();
    }
}
