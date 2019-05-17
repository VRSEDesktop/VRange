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
				Exercise.whiteboard.CheckProgress();
			}
		}
	}

    public Gun leftGun, rightGun;

    public bool HasSetGUI { get; set; }

    protected float StartTime;
    protected Exercise Exercise;

	public virtual void OnStart()
    {
		leftGun?.Reload();
		rightGun?.Reload();

        Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();
        GetComponent<Transform>().gameObject.SetActive(true);
		Progress = ExerciseProgress.NotStarted;

		Exercise.whiteboard.ClearBoard();
		StartTime = Time.realtimeSinceStartup;
		Exercise.OnStart();
    }

	public virtual void OnUpdate()
	{
		if ((leftGun != null && !leftGun.HasAmmo()) || (rightGun != null && !rightGun.HasAmmo()))
		{
			Progress = ExerciseProgress.Succeeded;
		}
    }


	public virtual void Restart()
    {
        ScenarioLogs.Clear();

		Exercise.DeleteBulletHoles();
		Exercise.DeleteLines();
		Progress = ExerciseProgress.NotStarted;

		if(leftGun) leftGun.Reload();
		if (rightGun) rightGun.Reload();

		StartTime = Time.realtimeSinceStartup;
		Exercise.whiteboard.ClearBoard();
	}

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

	private void OnProgressChanged()
	{
		Debug.Log("ExersiseState::OnProgressChanged" + Progress.ToString());
		if(Progress == ExerciseProgress.Succeeded || Progress == ExerciseProgress.Failed)
		{
			BulletLines.ForceActive();
		}
	}

    

    


}
