﻿using TMPro;
using UnityEngine;
using Valve.VR;

public class LoadLevelButton : MonoBehaviour, IHitable
{
    public SteamVR_LoadLevel levelLoader;

    public string text;
    public string levelName;

    public void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = text;
    }

    public void OnHit(BulletHit bulletHit)
    {
        levelLoader.levelName = levelName;
        levelLoader.Trigger();
    }
}