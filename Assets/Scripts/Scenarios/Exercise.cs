using UnityEngine;

public class Exercise : MonoBehaviour
{
    public Gun rightGun;

    public TextMesh text;

    public bool HasSettedGUI { get; set; }

    public void Initialize()
    {
        rightGun = GameObject.FindGameObjectWithTag("RightGun").GetComponent<Gun>();
        text = GameObject.FindGameObjectWithTag("ShootingStats").GetComponent<TextMesh>();

    }
    public void UpdateGUI()
    {
        if (!rightGun.HasAmmo())
        {
            if (!HasSettedGUI)
            {
                DisplayStats();
                Scenario.Clear();
            }
        }
        else
        {
            HasSettedGUI = false;
        }
    }

    private void DisplayStats()
    {
        text.text = "Shooting State:";

        foreach (var hit in Scenario.GetHits())
        {
            text.text += "\n" + hit.part.ToString();
        }

        HasSettedGUI = true;
    }
}
