using System.Collections;
using UnityEngine;

public class Exercise1 : Exercise
{
    public Animation anim;

    void Start()
    {
        StartCoroutine(TurningCardBoard());
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
