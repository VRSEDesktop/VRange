﻿using System.Collections;
using UnityEngine;

public static class BulletLines
{
	private static GameObject Parent;

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

        lineObject.SetActive(true);
		Scenario.lines.Add(lineObject);
    }

    public static void SetActive(bool active)
    {
		if(Parent != null)
		{
			if (Scenario.lines.Count > 0)
			{
				for (int i = 0; i < Scenario.lines.Count; ++i)
				{
					Scenario.lines[i].SetActive(active);
				}
			}
		}
	}

    public static void Destroy()
    {
		GameObject.Destroy(Parent);
		Scenario.lines.Clear();
    }
}