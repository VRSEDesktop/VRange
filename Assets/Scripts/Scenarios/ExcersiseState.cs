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
	public IList<LoggedHit> hits;
    public Gun leftGun, rightGun;

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
            Exercise.Progress = ExerciseProgress.Succeeded;
			Debug.Log(ScenarioLogs.GetHits().Count);
            UpdateGUI();
        }
    }

    public virtual void Restart()
    {
        ScenarioLogs.Clear();
		Exercise.Progress = ExerciseProgress.NotStarted;

		if(leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

		StartTime = Time.realtimeSinceStartup;
		ClearBoard();
	}

    public virtual void OnExit()
    {
        GetComponent<Transform>().gameObject.SetActive(false);
		ActiveNumbers();
        ScenarioLogs.Clear();
		InitializeWhiteboard();
		HasSetGUI = false;
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
		Debug.Log("updategui()");
       if (!HasSetGUI)
       {
            DisplayStats();
		}
    }

	public void ClearBoard()
	{
		if(gHead) gHead.SetActive(false);
		if(gLeftarm) gLeftarm.SetActive(false);
		if(gLeftleg) gLeftleg.SetActive(false);
		if(gRightarm) gRightarm.SetActive(false);
		if(gRightleg) gRightleg.SetActive(false);
	}

	private void ActiveNumbers()
	{
		if (gHead) gHead.SetActive(true);
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
		Debug.Log("Displaystats()");
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

		Debug.Log("Torso: " + torso);
	}

	public void OnProgressChanged()
	{
		Debug.Log("ExersiseState::OnProgressChanged" + Progress.ToString());
		if(Exercise.Progress == ExerciseProgress.Succeeded || Exercise.Progress == ExerciseProgress.Failed)
		{
			BulletLines.ForceActive();

			if (ExplanationUI != null)
				ExplanationUI.SetActive(false);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = true;
		}
		if (Exercise.Progress == ExerciseProgress.NotStarted || Exercise.Progress == ExerciseProgress.Started)
		{
			if (ExplanationUI != null)
				ExplanationUI.SetActive(true);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = false;
		}
		if (Exercise.Progress == ExerciseProgress.Started)
		{
			OnStart();
			StartTime = Time.time;
			leftGun?.Reload();
			rightGun?.Reload();
		}
	}

    private bool AddLine(GameObject g, int amount)
    {
        if(amount == 0 || amount == -1)
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
		gHead.SetActive(false);
		gTorso.SetActive(false);
		gRightarm.SetActive(false);
		gLeftarm.SetActive(false);
		gRightleg.SetActive(false);
		gLeftleg.SetActive(false);
	}

    public TextMeshPro RetrieveTextMesh(GameObject item)
    {
        return item.GetComponentInChildren<TextMeshPro>();
    }
}
