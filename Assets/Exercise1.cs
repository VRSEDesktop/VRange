using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise1 : Exercise
{

    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurningCardBoard());
    }

    // Update is called once per frame
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
