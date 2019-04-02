using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise : MonoBehaviour
{
    public Gun rightGun;

    public TextMesh text;

    public IList<LoggedHit> hits;

    public bool HasSettedGUI { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateGUI()
    {
        if (!rightGun.HasAmmo())
        {
            if (!HasSettedGUI)
            {
                DisplayStats();
                Scenario.Clear();
            }
        }
        else
        {
            HasSettedGUI = false;
        }
    }

    public void DisplayStats()
    {
        hits = Scenario.GetHits();
        text.text = "Shooting State:";

        foreach (var hit in hits)
        {
            text.text += "\n" + hit.part.ToString();
        }

        HasSettedGUI = true;
    }
}
