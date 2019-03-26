using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public static IList<ScenarioLog> logs = new List<ScenarioLog>();

    public static IList<LoggedHit> GetHits()
    {
        IList<LoggedHit> hits = new List<LoggedHit>();

        foreach(ScenarioLog log in logs)
        {
            if (log is LoggedHit) hits.Add((LoggedHit)log);
        }

        return hits;
    }

    public static IList<LoggedShot> GetShotsFromGun(Gun gun)
    {
        IList<LoggedShot> shots = new List<LoggedShot>();

        foreach (ScenarioLog log in logs)
        {
            if (log is LoggedShot && ((LoggedShot)log).gun.Equals(gun)) shots.Add((LoggedShot)log);           
        }

        return shots;
    }
}
