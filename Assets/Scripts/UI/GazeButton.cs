using UnityEngine;

/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
    public void Activate()
    {
        SetActive();
    }

    public void OnHoverStart()
    {
        loadingAnimation.SetBool("Loading", true);
    }

    public void OnHoverEnd()
    {
        loadingAnimation.SetBool("Loading", true);
    }
}