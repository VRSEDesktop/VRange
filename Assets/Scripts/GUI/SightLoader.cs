using UnityEngine;

/// <summary>
/// Checks for IGazeable objects being gazed at and activates them accordingly.
/// </summary>
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
        loadingCircle.transform.localScale = new Vector3(0.1f/loadingCircle.transform.lossyScale.x, 0.1f / loadingCircle.transform.lossyScale.y, 0.1f / loadingCircle.transform.lossyScale.z);
    }

    public void Update()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxRange);

        if (hasHit)
        {
            if (hit.collider.gameObject.GetComponentInChildren<IGazeable>() != null)
            {
                IGazeable uiElement = hit.collider.gameObject.GetComponentInChildren<IGazeable>();

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
                    if (lastObject && lastObject.GetComponentInChildren<IGazeable>() != null) lastObject.GetComponentInChildren<IGazeable>().OnHoverEnd();
                    uiElement.OnHoverStart();
                }

                lastObject = hit.collider.gameObject;

                loadingCircle.transform.position = hit.point;
                loadingCircle.transform.rotation = Quaternion.LookRotation(hit.normal);

            }
            else ResetLoader();
        }
        else ResetLoader();
    }

    private void ResetLoader()
    {
        if (lastObject != null && lastObject.GetComponentInChildren<IGazeable>() != null) lastObject.GetComponentInChildren<IGazeable>().OnHoverEnd();
        lastObject = null;
        timer = 0;
        activated = false;
        loadingCircle.GetComponent<Animator>().SetBool("Loading", false);
    }
}
