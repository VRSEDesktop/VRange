using System.Collections.Generic;

public static class UI
{
    private static List<string> ActiveButtons;

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