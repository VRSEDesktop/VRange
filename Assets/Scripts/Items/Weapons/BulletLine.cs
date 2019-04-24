using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class BulletLine : MonoBehaviour
{
    /// <summary>
    /// The GameObject that represents the line in 3D space.
    /// </summary>
    private GameObject Line;
    /// <summary>
    /// A parent GameObject for hierarchy purposes and easily destroying all bullet lines.
    /// </summary>
    public static GameObject Parent { get; private set; }
    /// <summary>
    /// How long the line should be visible before disappearing. If null it will not disappear.
    /// </summary>
    private float? Lifespan;

    public void OnEnable()
    {
        Line = GetComponent<GameObject>();
    }

    /// <summary>
    /// Creates the line
    /// </summary>
    /// <param name="start">Starting position.</param>
    /// <param name="end">Ending position.</param>
    /// <param name="color">The color of the line.</param>
    /// <param name="lifespan">The time before the line should disappear. If null it will not disappear.</param>
    public void Create(Vector3 start, Vector3 end, Color color, float? lifespan = 5f)
    {
        Parent = GameObject.Find("ShotsRays");
        if (Parent == null) Parent = new GameObject("ShotsRays");

        GameObject.Instantiate(Line);
        const float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

        Line.transform.localScale = new Vector3(thickness, thickness, length);
        Line.transform.position = start + ((end - start) / 2);
        Line.transform.LookAt(end);

        Line.GetComponent<MeshRenderer>().material.color = color;
        Line.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Line.GetComponent<Collider>().enabled = false;

        Lifespan = lifespan;

        Line.SetActive(true);

        Scenario.lines.Add(this);
    }

    /// <summary>
    /// Updates the lines timer for disappearing. Should be called by update from a monobehaviour.
    /// </summary>
    public void Update()
    {
        if(Lifespan != null)
        {
            Lifespan -= Time.deltaTime;
            if (Lifespan <= 0) Line.SetActive(false);
        }
    }
}
