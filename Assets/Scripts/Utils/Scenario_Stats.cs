using System;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_Stats : MonoBehaviour
{
    /// <summary>
    /// The guns of the player
    /// </summary>
    public static Gun leftGun, rightGun;
    private static DateTime startTime;
    public static IList<RegisteredHit> hits = new List<RegisteredHit>();

    public struct RegisteredHit
    {
        public readonly IHitable target;
        public readonly HitboxType part;
        public readonly BulletHit bulletHit;
        public readonly TimeSpan time;

        public RegisteredHit(IHitable target, HitboxType part, BulletHit bulletHit)
        {
            this.bulletHit = bulletHit;
            this.target = target;
            this.part = part;
            this.bulletHit = bulletHit;

            time = DateTime.Now.Subtract(startTime);
        }
    }

    public void Start()
    {
        startTime = DateTime.Now;
    }

    public static void RegisterHit(IHitable target, HitboxType partHit, BulletHit bulletHit)
    {
        hits.Add(new RegisteredHit(target, partHit, bulletHit));
    }
}
