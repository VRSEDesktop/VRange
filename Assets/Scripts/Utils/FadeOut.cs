using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public bool ForceEnable = false;

	private Renderer Renderer;
	private IEnumerator Coroutine;

	public void OnEnable()
	{
		Renderer = gameObject.GetComponent<Renderer>();
	}

	public void Update()
	{
		if (ForceEnable && Coroutine != null)
			StopCoroutine(Coroutine);
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
			yield return new WaitForEndOfFrame();
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

			if(ForceEnable)
			{
				Renderer.material.SetColor("_BaseColor", startingColor);
				break;
			}
		}
		//So we don't have to set the color back when reenabling.
		Renderer.material.SetColor("_BaseColor", startingColor);
		if(!ForceEnable)
			gameObject.SetActive(false);
	}
}
