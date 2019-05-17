using UnityEngine;

/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool IsActive = false;
	[SerializeField] private Color ActiveColor;
	[SerializeField] private Texture2D ActiveTexture;
	private Texture2D DefaultTexture;

	public void Start()
	{
		DefaultTexture = (Texture2D) GetComponent<Renderer>().material.GetTexture("_BaseMap");
	}

    public override void Activate()
    {
		if (IsActive)
		{
			SetInactive();
			if (ActiveTexture) GetComponent<Renderer>().material.SetTexture("_BaseMap", DefaultTexture);
			else StartCoroutine(ChangeColor(DefaultColor, 0.25f));
		}
		else
		{
			SetActive();

			if (ActiveTexture) GetComponent<Renderer>().material.SetTexture("_BaseMap", ActiveTexture);
			else StartCoroutine(ChangeColor(ActiveColor, 0.25f));
		}
        IsActive = !IsActive;
    }

	public override void OnHoverStart()
	{
		if (!ActiveTexture) StartCoroutine(ChangeColor(HoverColor, 0.25f));
	}

	public override void OnHoverEnd()
	{
		if (!ActiveTexture) StartCoroutine(ChangeColor(IsActive ? ActiveColor : DefaultColor, 0.25f));
	}
}
