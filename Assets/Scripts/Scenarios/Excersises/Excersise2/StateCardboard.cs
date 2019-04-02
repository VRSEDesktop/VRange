using System.Collections;
using UnityEngine;

public class StateCardboard : ExcersiseState
{
    public Animation flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToFlipCardboard = 5f;

    [HideInInspector]
    public Gun rightGun;
    [HideInInspector]
    public TextMesh text;
    public bool HasSettedGUI { get; set; }

    public override void OnStart()
    {
        base.OnStart();

        rightGun = GameObject.FindGameObjectWithTag("RightGun").GetComponent<Gun>();
        text = GameObject.FindGameObjectWithTag("ShootingStats").GetComponent<TextMesh>();

        StartCoroutine(TurningCardBoard());
    }

    public override void OnUpdate()
    {
        UpdateGUI();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(timeToFlipCardboard);
        flipAnimation.Play();
    }

    private void UpdateGUI()
    {
        if (!rightGun.HasAmmo())
        {
            if (!HasSettedGUI)
            {
                DisplayStats();
                Scenario.Clear();
            }
        }
        else HasSettedGUI = false;
    }

    private void DisplayStats()
    {
        text.text = "Shooting State:";

        foreach (var hit in Scenario.GetHits())
        {
            text.text += "\n" + hit.part.ToString();
        }

        HasSettedGUI = true;
    }
}
