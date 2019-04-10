/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool isActive;

    public new void Activate()
    {
        if (isActive)
            SetInactive();
        else
            SetActive();
    }
}
