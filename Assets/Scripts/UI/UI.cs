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

	public static UIElement GetUIItem(string name)
	{
		foreach(UIElement element in GameObject.FindObjectsOfType<UIElement>())
		{
			if (element.Name == name) return element;
		}
		return null;
	}
}