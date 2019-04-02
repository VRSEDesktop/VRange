using System.Collections;
using UnityEngine;

public class Exercise2 : Exercise
{
    public Animation flipAnimation;
    /// <summary>
    /// Time in seconds after which the cardboard with shooting target will filp
    /// </summary>
    public float timeToFlipCardboard = 5f;

    void Start()
    {
        StartCoroutine(TurningCardBoard());
        Initialize();
    }

    void Update()
    {
        UpdateGUI();
    }

    private IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(timeToFlipCardboard);
        flipAnimation.Play();
    }
}
