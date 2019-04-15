/// <summary>
/// The interface for gameobjects that need logic depending on whether the player is looking at it.
/// </summary>
public interface IGazeable
{
    void Activate();
    void OnHoverStart();
    void OnHoverEnd();
}
