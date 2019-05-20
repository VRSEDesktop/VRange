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
					fade.StartFadeOut(0);
					//fade.Disable();
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
				foreach (Material mat in childRenderer.materials)
				{
					if (mat.shader.name == "dissolve")
					{
						StartCoroutine(UnDissolve(childRenderer, 2));
					}
					else
					{
						//Any other lwrp shader
						Color currentcolor;
						if (mat.shader.name == "GUI/3D Text Shader")
						{
							currentcolor = mat.GetColor("_Color");
							currentcolor.a = 0;

							mat.SetColor("_Color", currentcolor);
							mat.SetFloat("_Surface", 1);

							mat.renderQueue = 3000;
							mat.SetFloat("_ZWrite", 0);
							mat.SetFloat("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
							mat.SetFloat("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
						}
						else
						{
							currentcolor = mat.GetColor("_BaseColor");
							currentcolor.a = 0;

							mat.SetFloat("_Surface", 1);
							mat.SetColor("_BaseColor", currentcolor);

							mat.renderQueue = 3000;
							mat.SetFloat("_ZWrite", 0);
							mat.SetFloat("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
							mat.SetFloat("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
						}

						FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
						if (!fade)
							fade = childRenderer.gameObject.AddComponent<FadeOut>();
						fade.StartFadeIn(0, 2);
						//fade.Enable();
					}
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
