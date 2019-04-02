using System.Collections;
using UnityEngine;

public class Exercise2 : Exercise
{
    public Animation anim;

    void Start()
    {
        StartCoroutine(TurningCardBoard());
        Initialize();
    }

    void Update()
    {
        UpdateGUI();
    }

    IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(5f);
        anim.Play();
    }
}
