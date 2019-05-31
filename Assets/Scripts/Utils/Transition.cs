using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
	[SerializeField] private bool OnGameObjectEnable = false;
	[SerializeField] private float DefaultDuration = 2f;

	public void OnEnable()
	{
		if (OnGameObjectEnable) Enable();
	}

	public void Disable(float duration = 0f)
	{
		foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
		{
			if (childRenderer != null)
			{
				foreach (Material mat in childRenderer.materials)
				{
					if (mat.shader.name == "dissolve")
					{
						StartCoroutine(Dissolve(mat, duration > 0 ? duration : DefaultDuration));
					}
					else
					{
						//Any other lwrp shader or GUI 3D text
						FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
						if (!fade)
							fade = childRenderer.gameObject.AddComponent<FadeOut>();
						fade.StartFadeOut(0);
					}
				}
			}
		}
	}

	public void Enable(float duration = 0f)
	{
		foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>())
		{
			if (childRenderer != null)
			{
				foreach (Material mat in childRenderer.materials)
				{
					if (mat.shader.name == "dissolve")
					{
						StartCoroutine(UnDissolve(mat, duration > 0 ? duration : DefaultDuration));
					}
					else
					{
						FadeOut fade = childRenderer.gameObject.GetComponent<FadeOut>();
						if (!fade)
							fade = childRenderer.gameObject.AddComponent<FadeOut>();
						fade.StartFadeIn(0, 2);
					}
				}
			}
		}
	}

	private IEnumerator Dissolve(Material material, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		material.SetFloat("_Amount", 0);

		while (progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			material.SetFloat("_Amount", Mathf.Lerp(0, 1, progress / duration));
		}
		gameObject.SetActive(false);
	}

	private IEnumerator UnDissolve(Material material, float duration)
	{
		float startTime = Time.time;
		float progress = 0;

		material.SetFloat("_Amount", 1);

		while(progress < duration)
		{
			yield return null;
			progress = Time.time - startTime;
			material.SetFloat("_Amount", Mathf.Lerp(1, 0, progress / duration));
		}
	}
}
