using TMPro;
using UnityEngine;
using System.Collections.Generic;

public enum ExerciseProgress
{
    NotStarted,
    Started,
    Succeeded,
    Failed
}

public abstract class ExcersiseState : MonoBehaviour
{
    public ExerciseProgress Progress;
    public Gun leftGun, rightGun;

	[HideInInspector]
    public GameObject gHead, gNeck, gTorso, gLeftarm, gRightarm, gLeftleg, gRightleg;

    private int head, torso, leftarm, rightarm, leftleg, rightleg, mis;
    private int AlignDistance = 20;

    public bool HasSetGUI { get; set; }

    protected float StartTime;
    protected Exercise Exercise;

    public virtual void OnStart()
    {
        Progress = ExerciseProgress.NotStarted;
        Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();
        GetComponent<Transform>().gameObject.SetActive(true);
        InitializeWhiteboard();
        StartTime = Time.realtimeSinceStartup;
        leftGun?.Reload();
        rightGun?.Reload();
    }

    public virtual void OnUpdate()
    {
        if(!leftGun.HasAmmo() || !rightGun.HasAmmo())
        {
            Progress = ExerciseProgress.Succeeded;
            UpdateGUI();
        }
    }

    public virtual void Restart()
    {
        Scenario.Clear();

       
        leftGun?.Reload();
        rightGun?.Reload();
        StartTime = Time.realtimeSinceStartup;

        HasSetGUI = false;
    }

    public virtual void OnExit()
    {
        GetComponent<Transform>().gameObject.SetActive(false);

        Scenario.Clear();

        HasSetGUI = false;
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
       if (!HasSetGUI)
       {
            DisplayStats();
            BulletLines.SetActive(true);
       }
       else HasSetGUI = false;
    }

    /// <summary>
    /// Sets the gui
    /// </summary>
    private void DisplayStats()
    {
        float time = Time.realtimeSinceStartup - StartTime;

        ResetGUI();
        ConvertingHits();

        // Header
        //AddLine("Tijd:", time.ToString("0.00") + " s");
        //AddLine("Schoten", "Aantal");

        // The stats
        if(gHead != null)AddLine(gHead, head);
        if(gTorso != null)AddLine(gTorso, torso);
        if(gLeftarm != null)AddLine(gLeftarm, leftarm);
        if (gRightarm != null) AddLine(gRightarm, rightarm);
        if (gLeftleg != null) AddLine(gLeftleg, leftleg);
        if (gRightarm != null) AddLine(gRightleg, rightleg);

        HasSetGUI = true;
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

    private bool AddLine(GameObject g, int amount)
    {
        if(amount == 0)
        {
            g.SetActive(false);
        }
        else
        {
            g.SetActive(true);
            RetrieveTextMesh(g).text = amount.ToString();
        }
        return true;
    }

    public void InitializeWhiteboard()
    {
        List<GameObject> whiteboardparts = new List<GameObject>();
        whiteboardparts.AddRange(GameObject.FindGameObjectsWithTag("WhiteboardPart"));

        foreach (var item in whiteboardparts)
        {
            string name = item.name;

            if (name == "Head")
            {
                gHead = item;
                gHead.SetActive(false);
            } else if (name == "Neck")
            {

            } else if (name == "Torso")
            {
                gTorso = item;
                gTorso.SetActive(false);
            } else if (name == "Leftarm")
            {
                gLeftarm = item;
                gLeftarm.SetActive(false);
            } else if (name == "Rightarm")
            {
                gRightarm = item;
                gRightarm.SetActive(false);
            }else if(name == "Rightleg")
            {
                gRightleg = item;
                gRightleg.SetActive(false);
            }else if(name == "Leftleg")
            {
                gLeftleg = item;
                gLeftleg.SetActive(false);
            }
        }
    }

    public TextMeshPro RetrieveTextMesh(GameObject item)
    {
        return item.GetComponentInChildren<TextMeshPro>();
    }
}
