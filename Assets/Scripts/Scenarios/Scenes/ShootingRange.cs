﻿using UnityEngine;
using Valve.VR;

public class ShootingRange : MonoBehaviour
{
    public SteamVR_LoadLevel levelLoader;

    void Update()
    {
        HandleButtons();
    }

    private void HandleButtons()
    {
        if (UI.GetButtonActivated("MainMenu"))
        {
            Scenario.Clear();
            levelLoader.levelName = "MainMenu";
            levelLoader.Trigger();
        }
    }
}