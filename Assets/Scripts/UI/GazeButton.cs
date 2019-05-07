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
    }

    public virtual void OnHoverStart()
	{
		StartCoroutine(ChangeColor(HoverColor, 1f));
	}

    public virtual void OnHoverEnd()
	{
		StartCoroutine(ChangeColor(DefaultColor, 1f));
	}
}