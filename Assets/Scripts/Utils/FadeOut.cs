using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public bool ForceEnable = false;
	private Renderer Renderer;
	private IEnumerator Coroutine;
	private float DefaultAlpha;
	private bool ShouldDisappear = false;

	public void OnEnable()
	{
		Renderer = gameObject.GetComponent<Renderer>();
		DefaultAlpha = Renderer.material.GetColor("_BaseColor").a;
	}

	public void Update()
	{
		if (ForceEnable && Coroutine != null)
		{
			StopCoroutine(Coroutine);
			Enable();
		}
	}

    public void StartFadeOut(float delay)
	{
		Coroutine = DisableLine(delay, 2);
		if(gameObject.activeSelf)
			StartCoroutine(Coroutine);
	}

	public void StartFadeIn(float delay)
	{
		Coroutine = EnableLine(delay, 2);
		if (gameObject.activeSelf)
			StartCoroutine(Coroutine);
	}

	private IEnumerator DisableLine(float delay, float fadeoutduration)
	{
		yield return new WaitForSeconds(delay);
		ShouldDisappear = true;

		float lerpStarttime = Time.time;
		float lerpProgress;
		Color startingColor = Renderer.material.GetColor("_BaseColor");
		Color endingColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0);

		while(true)
		{
			yield return null;

			if (ForceEnable && Renderer != null)
			{
				Enable();
				break;
			}

			lerpProgress = Time.time - lerpStarttime;
			if(Renderer != null)
			{
				Renderer.material.SetColor("_BaseColor", Color.Lerp(startingColor, endingColor, lerpProgress / fadeoutduration));
			}
			else
			{
				break;
			}

			if (lerpProgress >= fadeoutduration)
				break;
		}
	}

	private IEnumerator EnableLine(float delay, float fadeoutduration)
	{
		yield return new WaitForSeconds(delay);
		ShouldDisappear = true;

		float lerpStarttime = Time.time;
		float lerpProgress;
		Color startingColor = Renderer.material.GetColor("_BaseColor");
		Color endingColor = new Color(startingColor.r, startingColor.g, startingColor.b, 255);
		while (true)
		{
			yield return null;

			if (ForceEnable && Renderer != null)
			{
				Enable();
				break;
			}

			lerpProgress = Time.time - lerpStarttime;
			if (Renderer != null)
			{
				Renderer.material.SetColor("_BaseColor", Color.Lerp(startingColor, endingColor, lerpProgress / fadeoutduration));
			}
			else
			{
				break;
			}

			if (lerpProgress >= fadeoutduration)
				break;
		}
	}

	public void Enable()
	{
		if(Renderer != null)
		{
			if (!ShouldDisappear || ForceEnable)
			{
				Color currentColor = Renderer.material.GetColor("_BaseColor");
				Renderer.material.SetColor("_BaseColor", new Color(currentColor.r, currentColor.g, currentColor.b, DefaultAlpha));
			}
		}
	}

	public void Disable()
	{
		if(Renderer != null)
		{
			Color currentColor = Renderer.material.GetColor("_BaseColor");
			Renderer.material.SetColor("_BaseColor", new Color(currentColor.r, currentColor.g, currentColor.b, 0));
		}
	}
}
