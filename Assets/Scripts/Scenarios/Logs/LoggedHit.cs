using UnityEngine;

public class LoggedHit : ScenarioLog
{
    public readonly IHitable target;
    public readonly HitboxType part;
    public readonly Gun gun;
    public readonly RaycastHit raycastHit;

    public LoggedHit(IHitable target, HitboxType part, Gun gun, RaycastHit raycastHit) : base()
    {
        this.gun = gun;
        this.raycastHit = raycastHit;
        this.target = target;
        this.part = part;
    }
}