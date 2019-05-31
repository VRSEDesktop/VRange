using UnityEngine;
using System.Collections.Generic;

public abstract class ExcersiseState : MonoBehaviour
{
	private Gun leftGun, rightGun;

	public IList<LoggedHit> hits;

    public bool HasSetGUI { get; set; }

    protected float StartTime;
    protected Exercise Exercise;

	public virtual void OnInitialize()
	{
		Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();
		rightGun = Exercise.Player.rightHand.gun;
		leftGun = Exercise.Player.leftHand.gun;
		if(rightGun) rightGun.RemoveAmmo();
		if(leftGun) leftGun.RemoveAmmo();
		GetComponent<Transform>().gameObject.SetActive(true);
		Exercise.Progress = ExerciseProgress.NotStarted;

		Exercise.whiteboard.ClearBoard();
		Exercise.StartButton.SetState(true);
		Exercise.RestartButton.SetState(false);
	}

	public virtual void OnStart()
    {
		if (leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

		StartTime = Time.realtimeSinceStartup;
		BulletLines.SetActive(Exercise.Settings.DrawLines);
		Exercise.Progress = ExerciseProgress.Started;
		Exercise.StartButton.SetState(false);
		Exercise.RestartButton.SetState(true);
	}

	public virtual void OnUpdate()
	{
		if (Exercise.Progress == ExerciseProgress.NotStarted) return;

		if ((leftGun != null && !leftGun.HasAmmo()) || (rightGun != null && !rightGun.HasAmmo()))
		{
			Exercise.Progress = ExerciseProgress.Succeeded;
		}
    }

	public virtual void Restart()
    {
		Exercise.Clear();
		Exercise.Progress = ExerciseProgress.Started;

		if (leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

		StartTime = Time.realtimeSinceStartup;
		Exercise.whiteboard.ClearBoard();
	}

	public virtual void OnFinish() {}

	public virtual void OnExit()
    {
        GetComponent<Transform>().gameObject.SetActive(false);

		if (Exercise)
		{
			Exercise.whiteboard.ClearBoard();
			Exercise.Clear();
		}
	}

	public void OnProgressChanged()
	{
		Exercise.whiteboard.CheckProgress();

		if (Exercise.Progress == ExerciseProgress.Succeeded || Exercise.Progress == ExerciseProgress.Failed)
		{
			BulletLines.ForceActive();
			OnFinish();
		}
	}
}
