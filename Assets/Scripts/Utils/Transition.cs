using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
	public void Disable()
	{
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
		{
			if (childRenderer != null)
			{
				if(childRenderer.material.shader.name == "dissolve")
				{
					StartCoroutine(Dissolve(childRenderer, 2));
				}
				else
				{
					//Any other lwrp shader or GUI 3D text
					FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
					if (!fade)
						fade = childRenderer.gameObject.AddComponent<FadeOut>();
					//fade.StartFadeOut(0);
					fade.Disable();
				}
			}
		}
	}

	public void Enable()
	{
		foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
		{
			if (childRenderer != null)
			{
				if (childRenderer.material.shader.name == "dissolve")
				{
					StartCoroutine(UnDissolve(childRenderer, 2));
				}
				else
				{
					//Any other lwrp shader
					Color currentcolor;
					if(childRenderer.material.shader.name == "GUI/3D Text Shader")
					{
						currentcolor = childRenderer.material.GetColor("_Color");
						currentcolor.a = 0;
						childRenderer.material.SetColor("_Color", currentcolor);
					}	
					else
					{
						currentcolor = childRenderer.material.GetColor("_BaseColor");
						currentcolor.a = 0;
						childRenderer.material.SetColor("_BaseColor", currentcolor);
					}

					FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
					if (!fade)
						fade = childRenderer.gameObject.AddComponent<FadeOut>();
					//fade.StartFadeIn(0, 2);
					fade.Enable();
				}
			}
		}
	}

	private IEnumerator Dissolve(Renderer renderer, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		renderer.material.SetFloat("_Amount", 0);

		while (progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			renderer.material.SetFloat("_Amount", Mathf.Lerp(0, 1, progress / duration));
		}
		gameObject.SetActive(false);
	}

	private IEnumerator UnDissolve(Renderer renderer, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		renderer.material.SetFloat("_Amount", 1);

		while(progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			renderer.material.SetFloat("_Amount", Mathf.Lerp(1, 0, progress / duration));
		}
	}
}
