using UnityEngine;

public class SightLoader : MonoBehaviour
{
    private static int layer;
    private static readonly int maxRange = 50;
    private static readonly float timeToLoad = 2f;

    /// <summary>
    /// Object which was in sight line in the last frame
    /// </summary>
    private GameObject lastObject;
    private SpriteRenderer loadingCircle;

    /// <summary>
    /// Time player is looking at specific UI object
    /// </summary>
    private float timer = 0;
    private bool activated = false;

    public void Start()
    {
        layer = LayerMask.NameToLayer("UI");
        loadingCircle = GetComponentInChildren<SpriteRenderer>();
    }

    public void Update()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxRange);

        if (hasHit)
        {
            if (hit.collider.gameObject.GetComponentInChildren<ISightActivable>() != null)
            {
                ISightActivable uiElement = hit.collider.gameObject.GetComponentInChildren<ISightActivable>();

                if (hit.collider.gameObject.Equals(lastObject))
                {
                    timer += Time.deltaTime;

                    if (timer >= timeToLoad && !activated)
                    {
                        uiElement.Activate();
                        activated = true;
                        loadingCircle.GetComponent<Animator>().SetBool("Loading", true);
                        uiElement.OnHoverEnd();
                    }
                }
                else
                {
                    loadingCircle.GetComponent<Animator>().SetBool("Loading", true);
                    timer = 0;
                    if (lastObject && lastObject.GetComponentInChildren<ISightActivable>() != null) lastObject.GetComponentInChildren<ISightActivable>().OnHoverEnd();
                    uiElement.OnHoverStart();
                }

                lastObject = hit.collider.gameObject;

                loadingCircle.transform.position = hit.point;
                loadingCircle.transform.rotation = Quaternion.LookRotation(hit.normal);

            }
            else ResetLoader();
        }
        else ResetLoader();

        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.parent = transform;

        float thickness = 0.005f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + transform.rotation * Vector3.forward * 10;
        float length = Vector3.Distance(start, end);

        obj.transform.localScale = new Vector3(thickness, thickness, length);
        obj.transform.position = start + ((end - start) / 2);
        obj.transform.LookAt(end);

        obj.GetComponent<MeshRenderer>().material.color = Color.black;
        obj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        obj.GetComponent<Collider>().enabled = false;

        obj.SetActive(true);
        Destroy(obj, 1);
    }

    private void ResetLoader()
    {
        if (lastObject != null && lastObject.GetComponentInChildren<ISightActivable>() != null) lastObject.GetComponentInChildren<ISightActivable>().OnHoverEnd();
        lastObject = null;
        timer = 0;
        activated = false;
        loadingCircle.GetComponent<Animator>().SetBool("Loading", false);
    }
}
