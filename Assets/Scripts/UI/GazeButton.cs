using UnityEngine;
/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
	public Color HoverColor;
	private Color DefaultColor;

	public virtual void OnEnable()
	{
		DefaultColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
	}

    public virtual void Activate()
    {
        SetActive();
    }

    public virtual void OnHoverStart()
	{
		StartCoroutine(UI.ChangeColor(gameObject.GetComponent<Renderer>(), HoverColor, 0.25f));
	}

    public virtual void OnHoverEnd()
	{
		StartCoroutine(UI.ChangeColor(gameObject.GetComponent<Renderer>(), DefaultColor, 0.25f));
	}
}