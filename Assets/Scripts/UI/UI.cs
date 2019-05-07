using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI
{
    private static List<string> ActiveButtons = new List<string>();

    public static bool GetButtonActivated(string name)
    {
        return ActiveButtons.Contains(name);
    }

    public static void ActivateButton(string name)
    {
        ActiveButtons.Add(name);
    }

    public static void DeactivateButton(string name)
    {
        ActiveButtons.Remove(name);
    }
}