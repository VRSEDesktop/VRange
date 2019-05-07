using System.Collections;
using UnityEngine;
/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool IsActive;
	[SerializeField] private Color ActiveColor;
	private Color DefaultColor;

	public override void OnEnable()
	{
		DefaultColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
	}

    public override void Activate()
    {
		if (IsActive)
		{
			SetInactive();
			StartCoroutine(UI.ChangeColor(gameObject.GetComponent<Renderer>(), DefaultColor, 0.25f));
		}
		else
		{
			SetActive();
			StartCoroutine(UI.ChangeColor(gameObject.GetComponent<Renderer>(), ActiveColor, 0.25f));
		}
        IsActive = !IsActive;
    }
}
