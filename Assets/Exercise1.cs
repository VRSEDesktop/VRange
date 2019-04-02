using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise1 : MonoBehaviour
{
    public Gun rightGun;

    public TextMesh text;

    public IList<LoggedHit> hits;

    public bool HasSettedGUI { get; set; }

    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurningCardBoard());
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightGun.HasAmmo())
        {
            DisplayStats();
        }
    }

    void DisplayStats()
    {
        hits = Scenario.GetHits();
        text.text = "Shooting State:";

        foreach (var hit in hits)
        {
            text.text += "\n" + hit.part.ToString();
        }
    }

    IEnumerator TurningCardBoard()
    {
        yield return new WaitForSeconds(5f);
        anim.Play();
    }
}
