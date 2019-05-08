using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletLines
{
	public static bool Active;
	private static GameObject Parent;
	private static IList<GameObject> Lines = new List<GameObject>();

	/// <summary>
	/// Creates a line.
	/// </summary>
	/// <param name="start">Starting position.</param>
	/// <param name="end">Ending position.</param>
	/// <param name="color">The color of the line.</param>
	/// <param name="lifespan">The time before the line should disappear. If null it will not disappear.</param>
	public static void SpawnLine(GameObject linePrefab, Vector3 start, Vector3 end, Color color, float? lifespan = 5f)
    {
        if (Parent == null) Parent = new GameObject("ShotRays");

        const float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

		GameObject lineObject = GameObject.Instantiate(linePrefab);

		lineObject.transform.parent = Parent.transform;
		lineObject.transform.localScale = new Vector3(thickness, thickness, length);
		lineObject.transform.position = start + ((end - start) / 2);
		lineObject.transform.LookAt(end);

		lineObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lineObject.GetComponent<Collider>().enabled = false;
		lineObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color);

        if(lifespan != null)
		{
			lineObject.GetComponent<FadeOut>().StartFadeOut((float)lifespan);
		}

		if (!Active)
			lineObject.GetComponent<FadeOut>().Disable();

		Lines.Add(lineObject);
    }

    public static void SetActive(bool active)
    {
		Active = active;
		if(Parent != null)
		{
			if (Lines.Count > 0)
			{
				for (int i = 0; i < Lines.Count; ++i)
				{
					try
					{
						Lines[i].GetComponent<FadeOut>().Enable();
						if (!active)
							Lines[i].GetComponent<FadeOut>().Disable();
					}
					catch
					{
						Lines.RemoveAt(i);
					}
				}
			}
		}
	}

	public static void ForceActive()
	{
		Active = true;
		if (Parent != null)
		{
			if (Lines.Count > 0)
			{
				for (int i = 0; i < Lines.Count; ++i)
				{
					try
					{
						Lines[i].GetComponent<FadeOut>().ForceEnable = true;
					}
					catch
					{
						Lines.RemoveAt(i);
					}
				}
			}
		}
	}

    public static void Destroy()
    {
		GameObject.Destroy(Parent);
		Lines.Clear();
    }
}
