using UnityEngine;

public class SightLoader : MonoBehaviour
{
    private static readonly int layer = LayerMask.NameToLayer("UI");
    private static readonly int maxRange = 50;
    private static readonly float timeToLoad = 3f;

    /// <summary>
    /// Object which was in sight line in the last frame
    /// </summary>
    private GameObject lastObject;

    /// <summary>
    /// Time player is looking at specific UI object
    /// </summary>
    private float timer = 0;

    public void Update()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxRange, layer);

        if (hasHit && hit.collider.gameObject.GetComponent<ISightActivable>() != null)
        {
            ISightActivable uiElement = hit.collider.gameObject.GetComponent<ISightActivable>();

            Debug.Log(hit.collider.transform.name);

            if (lastObject.Equals(hit.collider.gameObject)) timer += Time.deltaTime;
            else timer = 0;

            lastObject = hit.collider.gameObject;

            if (timer >= timeToLoad) uiElement.Activate();
            uiElement.Draw();
        }
        else
        {
            lastObject = null;
            timer = 0;
        }
    }
}
