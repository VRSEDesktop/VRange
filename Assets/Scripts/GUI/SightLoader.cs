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

    /// <summary>
    /// Time player is looking at specific UI object
    /// </summary>
    private float timer = 0;
    private bool activated = false;

    public void Start()
    {
        layer = LayerMask.NameToLayer("UI");
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
                        uiElement.OnHoverEnd();
                    }
                }
                else
                {
                    timer = 0;
                    if (lastObject && lastObject.GetComponentInChildren<IGazeable>() != null) lastObject.GetComponentInChildren<IGazeable>().OnHoverEnd();
                    uiElement.OnHoverStart();
                }

                lastObject = hit.collider.gameObject;
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
    }
}
