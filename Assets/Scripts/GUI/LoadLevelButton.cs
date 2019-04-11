using Valve.VR;

public class LoadLevelButton : Button
{
    public SteamVR_LoadLevel levelLoader;
    public string levelName;

    public override void Activate()
    {
        Scenario.Clear();

        levelLoader.levelName = levelName;
        levelLoader.Trigger();
    }
}