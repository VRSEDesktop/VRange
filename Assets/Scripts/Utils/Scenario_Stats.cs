using System;
using System.Collections;
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

        public RegisteredHit(Enemy enemy, HitboxType partHit, BulletHit bulletHit)
        {
            this.bulletHit = bulletHit;
            this.target = enemy;
            this.part = partHit;
            this.bulletHit = bulletHit;

            time = DateTime.Now.Subtract(startTime);
        }
    }

    public void Start()
    {
        startTime = DateTime.Now;
    }

    public static void RegisterHit(Enemy enemy, HitboxType partHit, BulletHit bulletHit)
    {
        hits.Add(new RegisteredHit(enemy, partHit, bulletHit));
    }
}
