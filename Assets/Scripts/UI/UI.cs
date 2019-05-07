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

	public static IEnumerator ChangeColor(Renderer renderer, Color endingColor, float duration)
	{
		float lerpStarttime = Time.time;
		float lerpProgress;
		Color startingColor = renderer.material.GetColor("_BaseColor");
		while (true)
		{
			yield return new WaitForEndOfFrame();

			lerpProgress = Time.time - lerpStarttime;
			if (renderer != null)
			{
				renderer.material.SetColor("_BaseColor", Color.Lerp(startingColor, endingColor, lerpProgress / duration));
			}
		}
	}
}