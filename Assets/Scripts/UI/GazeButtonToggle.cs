using System.Collections;
using UnityEngine;
/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool IsActive = false;
	[SerializeField] private Color ActiveColor;

    public override void Activate()
    {
		if (IsActive)
		{
			SetInactive();
			StartCoroutine(ChangeColor(DefaultColor, 0.25f));
		}
		else
		{
			SetActive();
			StartCoroutine(ChangeColor(ActiveColor, 0.25f));
		}
        IsActive = !IsActive;
    }

	public override void OnHoverStart()
	{
		StartCoroutine(ChangeColor(HoverColor, 0.25f));
	}

	public override void OnHoverEnd()
	{
		StartCoroutine(ChangeColor(IsActive ? ActiveColor : DefaultColor, 0.25f));
	}
}
