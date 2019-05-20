using UnityEngine;
using System.Collections.Generic;

public abstract class ExcersiseState : MonoBehaviour
{
	public IList<LoggedHit> hits;

    public Gun leftGun, rightGun;

    public bool HasSetGUI { get; set; }

    protected float StartTime;
    protected Exercise Exercise;

	public virtual void OnInitialize()
	{
		Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();
		GetComponent<Transform>().gameObject.SetActive(true);
		Exercise.Progress = ExerciseProgress.NotStarted;

		Exercise.whiteboard.ClearBoard();
		Exercise.StartButton.SetState(true);
		Exercise.RestartButton.SetState(false);
	}

	public virtual void OnStart()
    {
		leftGun?.Reload();
		rightGun?.Reload();

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
        ScenarioLogs.Clear();

		Exercise.DeleteBulletHoles();
		Exercise.DeleteLines();
		Exercise.Progress = ExerciseProgress.NotStarted;

		if(leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

		StartTime = Time.realtimeSinceStartup;
		Exercise.whiteboard.ClearBoard();
	}

	public virtual void OnFinish() {}

	public virtual void OnExit()
    {
        GetComponent<Transform>().gameObject.SetActive(false);
        ScenarioLogs.Clear();
		if(Exercise) Exercise.whiteboard.ClearBoard();
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
		Debug.Log("updategui()");
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
