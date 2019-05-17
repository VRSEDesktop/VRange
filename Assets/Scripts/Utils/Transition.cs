using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
	public void Disable()
	{
		for(int i = 0; i < transform.childCount; ++i)
		{
			Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
			if (childRenderer != null)
			{
				if(childRenderer.material.shader.name == "dissolve")
				{
					Dissolve(childRenderer, 2);
				}
				else
				{
					//Any other lwrp shader or GUI 3D text
					FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
					if (!fade)
						fade = childRenderer.gameObject.AddComponent<FadeOut>();
					fade.StartFadeIn(0);
				}
			}
		}
	}

	public void Enable()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Renderer childRenderer = transform.GetChild(i).GetComponent<Renderer>();
			if (childRenderer != null)
			{
				if (childRenderer.material.shader.name == "dissolve")
				{
					UnDissolve(childRenderer, 2);
				}
				else
				{
					//Any other lwrp shader
					FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
					if (!fade)
						fade = childRenderer.gameObject.AddComponent<FadeOut>();
					fade.StartFadeIn(0, .5f);
				}
			}
		}
	}

	private IEnumerator Dissolve(Renderer renderer, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		renderer.material.SetFloat("_Amount", 1);

		while (progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			renderer.material.SetFloat("_Amount", Mathf.Lerp(1, 0, progress / duration));
		}
		gameObject.SetActive(false);
	}

	private IEnumerator UnDissolve(Renderer renderer, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		renderer.material.SetFloat("_Amount", 0);

		while(progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			renderer.material.SetFloat("_Amount", Mathf.Lerp(0, 1, progress / duration));
		}
	}
}
