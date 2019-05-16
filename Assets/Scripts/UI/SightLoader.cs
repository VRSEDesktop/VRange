using UnityEngine;

/// <summary>
/// Checks for IGazeable objects being gazed at and activates them accordingly.
/// </summary>
public class SightLoader : MonoBehaviour
{
    private static readonly int MaxRange = 50;
    private static readonly float TimeToLoad = 2f;

    /// <summary>
    /// Object which was in sight line in the last frame
    /// </summary>
    private IGazeable LastUIElement;
    private SpriteRenderer LoadingCircle;

    /// <summary>
    /// Time player is looking at specific UI object
    /// </summary>
    private float Timer = 0;
    private bool Activated = false;

    public void Start()
    {
        LoadingCircle = GetComponentInChildren<SpriteRenderer>();
    }

    public void Update()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, MaxRange);

        if (hasHit && hit.collider.gameObject.GetComponentInChildren<IGazeable>() != null)
        {
            IGazeable currentUIElement = hit.collider.gameObject.GetComponentInChildren<IGazeable>();

			if (currentUIElement.Equals(LastUIElement))
			{
				Timer += Time.deltaTime;

				if (Timer >= TimeToLoad && !Activated) ActivateElement(currentUIElement);
			}
			else StartLoading(currentUIElement);

			UpdateLoading(hit);
		}
        else ResetLoader();
    }

	private void UpdateLoading(RaycastHit hit)
	{
		LastUIElement = hit.collider.gameObject.GetComponentInChildren<IGazeable>();

		Vector3 offset = hit.normal;
		offset.Scale(new Vector3(0.005f, 0.005f, 0.005f));
		Vector3 position = hit.point + offset;
		LoadingCircle.transform.position = position;

		LoadingCircle.transform.rotation = Quaternion.LookRotation(hit.normal);
	}

	/// <summary>
	/// Finishes loading and activates the element
	/// </summary>
	/// <param name="currentUIElement"></param>
	private void ActivateElement(IGazeable currentUIElement)
	{
		currentUIElement.Activate();
		Activated = true;
		LoadingCircle.GetComponent<Animator>().SetBool("Loading", false);
		currentUIElement.OnHoverEnd();
	}

	private void StartLoading(IGazeable currentUIElement)
	{
		LoadingCircle.GetComponent<Animator>().SetBool("Loading", true);
		Timer = 0;
		if (LastUIElement != null) LastUIElement.OnHoverEnd();
		currentUIElement.OnHoverStart();
	}

    private void ResetLoader()
    {
        LastUIElement?.OnHoverEnd();
        LastUIElement = null;
        Timer = 0;
        Activated = false;
        LoadingCircle.GetComponent<Animator>().SetBool("Loading", false);
    }
}
