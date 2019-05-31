using System.Collections;
using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    public string Name;

    public virtual void SetActive()
    {
        UI.ActivateButton(Name);
    }

    public virtual void SetInactive()
    {
        UI.DeactivateButton(Name);
    }

    public virtual void OnDisable()
    {
        SetInactive();
    }

	public virtual IEnumerator ChangeColor(Color endingColor, float duration)
	{
		Renderer renderer = gameObject.GetComponent<Renderer>();
		float lerpStarttime = Time.time;
		float lerpProgress;
		Color startingColor = renderer.material.GetColor("_BaseColor");

		while (true)
		{
			yield return null;

			lerpProgress = Time.time - lerpStarttime;
			if (renderer != null)
			{
				renderer.material.SetColor("_BaseColor", Color.Lerp(startingColor, endingColor, lerpProgress / duration));
			}
			if (lerpProgress >= duration)
				break;
		}
	}
}