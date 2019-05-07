using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public bool ForceEnable = false;
	private Renderer Renderer;
	private IEnumerator Coroutine;
	private float DefaultAlpha;

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

	private IEnumerator DisableLine(float delay, float fadeoutduration)
	{
		yield return new WaitForSeconds(delay);

		float lerpStarttime = Time.time;
		float lerpProgress;
		Color startingColor = Renderer.material.GetColor("_BaseColor");
		Color endingColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0);
		while(true)
		{
			yield return null;

			if (ForceEnable && Renderer != null)
			{
				Renderer.material.SetColor("_BaseColor", startingColor);
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

	public void Enable()
	{
		Color currentColor = Renderer.material.GetColor("_BaseColor");
		Renderer.material.SetColor("_BaseColor", new Color(currentColor.r, currentColor.g, currentColor.b, DefaultAlpha));
	}

	public void Disable()
	{
		Color currentColor = Renderer.material.GetColor("_BaseColor");
		Renderer.material.SetColor("_BaseColor", new Color(currentColor.r, currentColor.g, currentColor.b, 0));
	}
}
