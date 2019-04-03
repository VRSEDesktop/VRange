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
            if (hit.collider.gameObject.GetComponent<ISightActivable>() != null)
            {
                ISightActivable uiElement = hit.collider.gameObject.GetComponent<ISightActivable>();

                if (hit.collider.gameObject.Equals(lastObject)) timer += Time.deltaTime;
                else timer = 0;

                lastObject = hit.collider.gameObject;

                if (timer >= timeToLoad && !activated)
                {
                    uiElement.Activate();
                    activated = true;
                }
                else uiElement.Draw();
            }
            else
            {
                lastObject = null;
                timer = 0;
                activated = false;
            }
        }
        else
        {
            lastObject = null;
            timer = 0;
            activated = false;
        }
    }
}
