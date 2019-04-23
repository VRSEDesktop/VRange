/// <summary>
/// Gaze Button that toggles.
/// </summary>
public class GazeButtonToggle : GazeButton
{
    private bool isActive;

    public override void Activate()
    {
        if (isActive) SetInactive();
        else          SetActive();

        isActive = !isActive;
    }
}
