using System.Collections;
using UnityEngine;

public class Exercise2 : Exercise
{
    // state 0
    public Animation anim;
    public GameObject cardBoard;

    // state 1
    public GameObject npc;

    void Start()
    {
        StartCoroutine(TurningCardBoard());
        Initialize();
    }

    void Update()
    {
        UpdateGUI();
        UpdateState();
    }

    IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(5f);
        anim.Play();
    }

    void UpdateState()
    {
        switch (State)
        {
            case 0:

                break;

            case 1:
                cardBoard.SetActive(false);
                npc.SetActive(true);
                break;
        }
    }
}
