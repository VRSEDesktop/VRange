/// <summary>
/// Button that activates when gazed at.
/// </summary>
public class GazeButton : UIElement, IGazeable
{
    public virtual void Activate()
    {
        SetActive();
    }

    public virtual void OnHoverStart() {}

    public virtual void OnHoverEnd() {}
}