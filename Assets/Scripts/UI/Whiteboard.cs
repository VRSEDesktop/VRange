using TMPro;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
	public GameObject gHead, gNeck, gTorso, gLeftarm, gRightarm, gLeftleg, gRightleg, gMis;
	public GameObject ExplanationUI, FeedbackUI;

	public Exercise exercise;

	public Gun leftGun, rightGun;

	private int head, torso, leftarm, rightarm, leftleg, rightleg, mis;

	private bool HasSetGUI;

	public void Start()
	{
		if (ExplanationUI != null)
			ExplanationUI.SetActive(true);
		if (FeedbackUI != null)
			FeedbackUI.GetComponent<MeshRenderer>().enabled = false;

		ClearBoard();
	}

	public void Update()
	{
		CheckProgress();
	}

	public void CheckProgress()
	{
		if (exercise.Progress == ExerciseProgress.Succeeded || exercise.Progress == ExerciseProgress.Failed)
		{
			if (ExplanationUI != null)
				ExplanationUI.SetActive(false);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = true;

			if (!HasSetGUI)
			{
				DisplayStats();
				HasSetGUI = true;
			}
		}
		else
		{
			if (ExplanationUI != null)
				ExplanationUI.SetActive(true);
			if (FeedbackUI != null)
				FeedbackUI.GetComponent<MeshRenderer>().enabled = false;

			HasSetGUI = false;
		}
	}

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
		if (gMis != null) AddLine(gMis, mis);
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
			else if (hit.part.ToDescriptionString() == "Linkerarm") leftarm++;
			else if (hit.part.ToDescriptionString() == "Rechterarm") rightarm++;
			else if (hit.part.ToDescriptionString() == "Linkerbeen") leftleg++;
			else if (hit.part.ToDescriptionString() == "Rechterbeen") rightleg++;
		}
		
		mis = ScenarioLogs.GetShotsFromGun(CorrectGun()).Count - ScenarioLogs.GetHits().Count;
	}

	private Gun CorrectGun()
	{
		if(rightGun.currentAmmo != rightGun.magCapacity) return rightGun;
		else return leftGun;
	}

	private bool AddLine(GameObject g, int amount)
	{
		if (amount == 0 || amount == -1)
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

	public TextMeshPro RetrieveTextMesh(GameObject item)
	{
		return item.GetComponentInChildren<TextMeshPro>();
	}

	private void ActiveNumbers()
	{
		if (gHead) gHead.SetActive(true);
		if (gLeftarm) gLeftarm.SetActive(true);
		if (gLeftleg) gLeftleg.SetActive(true);
		if (gRightarm) gRightarm.SetActive(true);
		if (gRightleg) gRightleg.SetActive(true);
		if (gMis) gMis.SetActive(true);
	}

	public void ClearBoard()
	{
		if (gHead) gHead.SetActive(false);
		if (gLeftarm) gLeftarm.SetActive(false);
		if (gLeftleg) gLeftleg.SetActive(false);
		if (gRightarm) gRightarm.SetActive(false);
		if (gRightleg) gRightleg.SetActive(false);
		if (gTorso) gTorso.SetActive(false);
		if (gMis) gMis.SetActive(false);

		ResetGUI();

		HasSetGUI = false;
	}
}
