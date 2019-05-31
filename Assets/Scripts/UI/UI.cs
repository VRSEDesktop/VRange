using System.Collections.Generic;

public static class UI
{
    private static List<string> ActiveButtons = new List<string>();

    public static bool GetButtonActivated(string name)
    {
        return ActiveButtons.Contains(name);
    }

	public static bool GetButtonActivatedAndTurnOff(string name)
	{
		return ActiveButtons.Remove(name);
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