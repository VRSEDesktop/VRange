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
	private ExerciseProgress _progress;
	public IList<LoggedHit> hits;
    public ExerciseProgress Progress {
		get { return _progress; }
		set {
			if(value != _progress)
			{
				_progress = value;
				OnProgressChanged();
			}
		}
	}

    public Gun leftGun, rightGun;

	[HideInInspector]
    public GameObject gHead, gNeck, gTorso, gLeftarm, gRightarm, gLeftleg, gRightleg;
    private int head, torso, leftarm, rightarm, leftleg, rightleg, mis;

    public bool HasSetGUI { get; set; }

    protected float StartTime;
    protected Exercise Exercise;

	public GameObject ExplanationUI;
	public GameObject FeedbackUI;

    public virtual void OnStart()
    {
		leftGun?.Reload();
		rightGun?.Reload();

		Progress = ExerciseProgress.NotStarted;
        Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();
        GetComponent<Transform>().gameObject.SetActive(true);

		InitializeWhiteboard();

		StartTime = Time.realtimeSinceStartup;

		if (ExplanationUI != null)
			ExplanationUI.SetActive(true);
		if (FeedbackUI != null)
			FeedbackUI.GetComponent<MeshRenderer>().enabled = false;
    }

    public virtual void OnUpdate()
    {
        if((leftGun != null && !leftGun.HasAmmo()) || (rightGun != null && !rightGun.HasAmmo()))
        {
            Progress = ExerciseProgress.Succeeded;
			//Debug.Log(ScenarioLogs.GetHits().Count);
			UpdateGUI();
        }
    }

    public virtual void Restart()
    {
        ScenarioLogs.Clear();
		Progress = ExerciseProgress.NotStarted;

		ClearBoard();
		ResetGUI();

		if(leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

        StartTime = Time.realtimeSinceStartup;

        HasSetGUI = false;
    }

    public virtual void OnExit()
    {
        GetComponent<Transform>().gameObject.SetActive(false);
		ActiveNumbers();
        ScenarioLogs.Clear();

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
		}
		else
		{
			HasSetGUI = false;
		}
    }

	public void ClearBoard()
	{
		if(gHead) gHead.SetActive(false);
		if(gNeck) gNeck.SetActive(false);
		if(gLeftarm) gLeftarm.SetActive(false);
		if(gLeftleg) gLeftleg.SetActive(false);
		if(gRightarm) gRightarm.SetActive(false);
		if(gRightleg) gRightleg.SetActive(false);
	}

	private void ActiveNumbers()
	{
		if (gHead) gHead.SetActive(true);
		if (gNeck) gNeck.SetActive(true);
		if (gLeftarm) gLeftarm.SetActive(true);
		if (gLeftleg) gLeftleg.SetActive(true);
		if (gRightarm) gRightarm.SetActive(true);
		if (gRightleg) gRightleg.SetActive(true);
	}

	/// <summary>
	/// Sets the gui
	/// </summary>
	private void DisplayStats()
    {
        //float time = Time.realtimeSinceStartup - StartTime;

        ResetGUI();
        ConvertingHits();
		// Header
		//AddLine("Tijd:", time.ToString("0.00") + " s");
		//AddLine("Schoten", "Aantal");

		// The stats
		if (gHead != null) AddLine(gHead, head);
        if (gTorso != null) AddLine(gTorso, torso);
        if (gLeftarm != null) AddLine(gLeftarm, leftarm);
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
        foreach (var hit in ScenarioLogs.GetHits())
        {
			if (hit.part.ToDescriptionString() == "Hoofd") head++;
            else if (hit.part.ToDescriptionString() == "Torso") torso++;
            else if (hit.part.ToDescriptionString() == "Linkerarm") leftarm ++;
            else if (hit.part.ToDescriptionString() == "Rechterarm") rightarm++;
            else if (hit.part.ToDescriptionString() == "Linkerbeen") leftleg++;
            else if (hit.part.ToDescriptionString() == "Rechterbeen") rightleg++;
            else mis++;
        }
	}

	private void OnProgressChanged()
	{
		Debug.Log("ExersiseState::OnProgressChanged" + Progress.ToString());
		if(Progress == ExerciseProgress.Succeeded || Progress == ExerciseProgress.Failed)
		{
			BulletLines.ForceActive();

			if (ExplanationUI != null)
				ExplanationUI.SetActive(false);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			if(ExplanationUI != null)
				ExplanationUI.SetActive(true);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = false;
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
				gNeck = item;
				gNeck.SetActive(false);
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
