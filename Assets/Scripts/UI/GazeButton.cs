using System.Collections;
using UnityEngine;

/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
	public Color HoverColor;
	[HideInInspector] public Color DefaultColor;

	public void OnEnable()
	{
		DefaultColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
	}

	public virtual void Activate()
	{
		SetActive();
		StartCoroutine(DeactivateAfterFrame());
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
		catch
		{

		}
	}

	/// <summary>
	/// Waits one frame until deactivating.
	/// </summary>
	/// <returns>The IEnumerator used in MonoBehaviour::StartCoroutine</returns>
	private IEnumerator DeactivateAfterFrame()
	{
		//Waits one frame. For some reason WaitForEndOfFrame seems stable in the editor, while null seems stable in build.
#if UNITY_EDITOR
		yield return new WaitForEndOfFrame();
#else
		yield return null;
#endif
		SetInactive();
	}
}