using System.Collections.Generic;
using UnityEngine;

public class Exercise : MonoBehaviour
{
    public Gun rightGun;

    public TextMesh text;

    public IList<LoggedHit> hits;

    public bool HasSettedGUI { get; set; }

    public static int State { get; set; }

    public void Initialize()
    {
        rightGun = GameObject.FindGameObjectWithTag("RightGun").GetComponent<Gun>();
        text = GameObject.FindGameObjectWithTag("ShootingStats").GetComponent<TextMesh>();

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
