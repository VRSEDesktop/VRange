using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class BulletLine : MonoBehaviour
{
    /// <summary>
    /// How long the line should be visible before disappearing. If null it will not disappear.
    /// </summary>
    private float? Lifespan;
	/// <summary>
	/// Bool that makes sure lines aren't disabled again.
	/// </summary>
	public static bool ForceActive { get; private set; } = false;
	public static bool Enabled { private get; set; }

    /// <summary>
    /// Creates the line
    /// </summary>
    /// <param name="start">Starting position.</param>
    /// <param name="end">Ending position.</param>
    /// <param name="color">The color of the line.</param>
    /// <param name="lifespan">The time before the line should disappear. If null it will not disappear.</param>
    public void Create(Vector3 start, Vector3 end, Color color, float? lifespan = 5f)
    {
		GameObject parent = GameObject.Find("ShotRays");
        if (parent == null) parent = new GameObject("ShotRays");

        const float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

        gameObject.transform.parent = parent.transform;
        gameObject.transform.localScale = new Vector3(thickness, thickness, length);
        gameObject.transform.position = start + ((end - start) / 2);
        gameObject.transform.LookAt(end);

        gameObject.GetComponent<MeshRenderer>().material.color = color;
        gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", color);

        Lifespan = lifespan;

        gameObject.SetActive(true);

        Scenario.lines.Add(this);
    }

    public static void EnableAll()
    {
        ForceActive = true;
    }

    /// <summary>
    /// Updates the lines timer for disappearing. Should be called by update from a monobehaviour.
    /// </summary>
    public void Update()
    {
		if(!ForceActive)
		{
			if(Lifespan != null || Lifespan > 0)
			{
				Lifespan -= Time.deltaTime;
				if (Lifespan <= 0) gameObject.SetActive(false);
			}
		}
        else
		{
			gameObject.SetActive(true);
		}
    }

    public static void Destroy()
    {
        ForceActive = false;
		Destroy(GameObject.Find("ShotRays"));
    }
}
