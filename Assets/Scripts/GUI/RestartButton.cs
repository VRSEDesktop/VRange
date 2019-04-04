﻿using UnityEngine;

public class RestartButton : Button
{
    public override void Activate()
    {
        Scenario.Clear();
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Restart();
    }
}
