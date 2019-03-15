public class LoggedHit : ScenarioLog
{
    public readonly IHitable target;
    public readonly HitboxType part;
    public readonly BulletHit bulletHit;

    public LoggedHit(IHitable target, HitboxType part, BulletHit bulletHit) : base()
    {
        this.bulletHit = bulletHit;
        this.target = target;
        this.part = part;
    }
}