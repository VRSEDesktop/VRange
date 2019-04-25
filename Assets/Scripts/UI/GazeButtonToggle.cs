/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool IsActive;

    public override void Activate()
    {
        if (IsActive) SetInactive();
        else          SetActive();

        IsActive = !IsActive;
    }
}
