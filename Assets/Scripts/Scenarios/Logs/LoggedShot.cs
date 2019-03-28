public class LoggedShot : ScenarioLog
{
    public readonly Gun gun;
    public readonly bool succeeded;

    public LoggedShot(Gun gun, bool succeeded) : base()
    {
        this.gun = gun;
        this.succeeded = succeeded;
    }
}