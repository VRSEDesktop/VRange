using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ExcersiseState : MonoBehaviour
{
    public Gun leftGun, rightGun;
    [HideInInspector]
    public TextMeshPro text;
    public TextMeshPro text2;

    private int head, torso, leftarm, rightarm, leftleg, rightleg, mis;
    private int AlignDistance = 20;

    public bool HasSettedGUI { get; set; }

    protected float startTime;

    public virtual void OnStart()
    {
        GetComponent<Transform>().gameObject.SetActive(true);

        text = GameObject.FindWithTag("Shootingstats").GetComponentInChildren<TextMeshPro>();
        text2 = GameObject.FindGameObjectWithTag("Shootingstats2").GetComponentInChildren<TextMeshPro>();

        startTime = Time.realtimeSinceStartup;
        leftGun?.Reload();
        rightGun?.Reload();
    }

    public abstract void OnUpdate();

    public virtual void Restart()
    {     
        Scenario.Clear();

        leftGun?.Reload();
        rightGun?.Reload();
        startTime = Time.realtimeSinceStartup;
        text.text = "";
        text2.text = "";
        HasSettedGUI = false;
    }

    public virtual void OnExit()
    {
        if(text) text.text = "";
        GetComponent<Transform>().gameObject.SetActive(false);

        Scenario.Clear();
        HasSettedGUI = false;
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
        if (!leftGun.HasAmmo() || !rightGun.HasAmmo())
        {
            if (!HasSettedGUI)
            {
                DisplayStats();
            }
        }
        else HasSettedGUI = false;
    }

    /// <summary>
    /// Sets the gui
    /// </summary>
    private void DisplayStats()
    {

        float time = Time.realtimeSinceStartup - startTime;

        ResetGUI();
        ConvertingHits();

        // Header
        AddLine("Tijd:", time.ToString("0.00") + " s");
        AddLine("Schoten", "Aantal");

        // The stats
        AddLine("Mis", mis.ToString());
        AddLine("Hoofd", head.ToString());
        AddLine("Torso", torso.ToString());
        AddLine("Linkerarm", leftarm.ToString());
        AddLine("Rechterarm", rightarm.ToString());
        AddLine("Linkerbeen", leftleg.ToString());
        AddLine("Rechterbeen", rightleg.ToString());

        HasSettedGUI = true;
    }

    private void ResetGUI()
    {
        head = 0;
        torso = 0;
        leftarm = 0;
        rightarm = 0;
        leftleg = 0;
        rightleg = 0;
        mis = 0;

        text.text = "";
        text2.text = "";
    }

    private void ConvertingHits()
    {
        foreach (var hit in Scenario.GetHits())
        {
            if (hit.part.ToDescriptionString() == "Hoofd")
            {
                head += 1;
            }
            else if (hit.part.ToDescriptionString() == "Torso")
            {
                torso += 1;
            }
            else if (hit.part.ToDescriptionString() == "Linkerarm")
            {
                leftarm += 1;
            }
            else if (hit.part.ToDescriptionString() == "Rechterarm")
            {
                rightarm += 1;
            }
            else if (hit.part.ToDescriptionString() == "Linkerbeen")
            {
                leftleg += 1;
            }
            else if (hit.part.ToDescriptionString() == "Rechterbeen")
            {
                rightleg += 1;
            }
            else
            {
                mis += 1;
            }
        }
    }

    private bool AddLine(string Text, string Text2)
    {
        if(Text2 == "0")
        {
            return false;
        }
        else if (Text2 == "-1")
        {
            text.text += Text;
            text2.text += Text2;
        }
        else
        {
            text.text += Text;
            text2.text += Text2;
        }

        text.text += "\n";
        text2.text += "\n";
        return true;
    }
}