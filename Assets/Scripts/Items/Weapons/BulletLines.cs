using System.Collections;
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
    public static void SpawnLine(GameObject lineObject, Vector3 start, Vector3 end, Color color, float? lifespan = 5f)
    {
        if (Parent == null) Parent = new GameObject("ShotRays");

        const float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

		lineObject.transform.parent = Parent.transform;
		lineObject.transform.localScale = new Vector3(thickness, thickness, length);
		lineObject.transform.position = start + ((end - start) / 2);
		lineObject.transform.LookAt(end);

		lineObject.GetComponent<MeshRenderer>().sharedMaterial.color = color;
		lineObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lineObject.GetComponent<Collider>().enabled = false;
		lineObject.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", color);

        if(lifespan != null)
		{
			//start coroutine
		}

        lineObject.SetActive(true);
    }

    public static void SetActive(bool active)
    {
		GameObject[] children = Parent.GetComponentsInChildren<GameObject>();
		if (children != null)
		{
			for (int i = 0; i < children.Length; ++i)
			{
				children[i].SetActive(active);
			}
		}
	}

    public static void Destroy()
    {
		GameObject.Destroy(Parent);
    }
}
