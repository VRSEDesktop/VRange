﻿using TMPro;
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
	}

	public void LateUpdate()
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
		if(isActiveAndEnabled) StartCoroutine(ChangeColor(DefaultColor, 1f));
	}

	/// <summary>
	/// Used to make the button deactive. Fixes errors connected with animations and deactivated button objects.
	/// </summary>
	/// <param name="_isVisible"></param>
	public void SetState(bool _isVisible)
	{
		GetComponent<MeshRenderer>().enabled = _isVisible;
		if(GetComponentInChildren<TextMeshPro>()) GetComponentInChildren<TextMeshPro>().enabled = _isVisible;
		GetComponent<Collider>().enabled = _isVisible;
	}
}