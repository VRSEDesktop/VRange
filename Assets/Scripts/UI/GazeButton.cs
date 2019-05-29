using TMPro;
using UnityEngine;

/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
	private bool activated;
	private int framesSinceActive;

	[HideInInspector] public Color DefaultColor;
	public Color HoverColor;

	public virtual void OnEnable()
	{
		DefaultColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
	}

	public virtual void Activate()
	{
		SetActive();
		activated = true;
		framesSinceActive = Time.frameCount;

		Debug.Log("Activate() " + Name);
	}

	public void LateUpdate()
	{
		if (activated && framesSinceActive < Time.frameCount)
		{
			activated = false;
			SetInactive();

			Debug.Log("Deactivate() " + Name);
		}
	}

	public virtual void OnHoverStart()
	{
		StartCoroutine(ChangeColor(HoverColor, 1f));
	}

	public virtual void OnHoverEnd()
	{
		if(isActiveAndEnabled) StartCoroutine(ChangeColor(DefaultColor, 1f));
	}

	public void SetState(bool _isVisible)
	{
		activated = !_isVisible;
		GetComponent<MeshRenderer>().enabled = _isVisible;
		if(GetComponentInChildren<TextMeshPro>()) GetComponentInChildren<TextMeshPro>().enabled = _isVisible;
		GetComponent<Collider>().enabled = _isVisible;
	}
}