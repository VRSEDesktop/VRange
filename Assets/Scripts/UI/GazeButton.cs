using UnityEngine;

/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
	public Color HoverColor;
	private bool activated;
	private int framesSinceActive = 0;
	[HideInInspector] public Color DefaultColor;

	public void OnEnable()
	{
		DefaultColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
	}

	public virtual void Activate()
	{
		SetActive();
		activated = true;
		framesSinceActive = Time.frameCount;
	}

	public void Update()
	{
		if (activated && framesSinceActive < Time.frameCount)
		{
			activated = false;
			SetInactive();
		}
	}

	public virtual void OnHoverStart()
	{
		StartCoroutine(ChangeColor(HoverColor, 1f));
	}

	public virtual void OnHoverEnd()
	{
		try
		{
			StartCoroutine(ChangeColor(DefaultColor, 1f));
		}
		catch {} // Scene might be changed and references removed, while coroutine is still working
	}
}