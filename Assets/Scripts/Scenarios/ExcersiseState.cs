using TMPro;
using UnityEngine;

public abstract class ExcersiseState : MonoBehaviour
{
    public Gun leftGun, rightGun;
    [HideInInspector]
    public TextMeshPro text;
    public bool HasSettedGUI { get; set; }
    public bool DrawPreviousScenarioButton, DrawNextScenarioButton;
    private Exercise Exercise;
    protected float StartTime;

    public virtual void OnStart()
    {
        GetComponent<Transform>().gameObject.SetActive(true);
        Exercise = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>();

        text = GameObject.FindWithTag("ShootingStats").GetComponentInChildren<TextMeshPro>();

        StartTime = Time.realtimeSinceStartup;
        leftGun?.Reload();
        rightGun?.Reload();

        Exercise.PreviousScenario.gameObject.SetActive(DrawPreviousScenarioButton);
        Exercise.NextScenario.gameObject.SetActive(DrawNextScenarioButton);
    }

    public abstract void OnUpdate();

    public virtual void Restart()
    {     
        Scenario.Clear();

        leftGun?.Reload();
        rightGun?.Reload();
        StartTime = Time.realtimeSinceStartup;
        text.text = "";
        HasSettedGUI = false;
    }

    public virtual void OnExit()
    {
        if(text) text.text = "";
        GetComponent<Transform>().gameObject.SetActive(false);

        Scenario.Clear();
        HasSettedGUI = false;
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
        if (!leftGun.HasAmmo() || !rightGun.HasAmmo())
        {
            if (!HasSettedGUI)
            {
                DisplayStats();
            }
        }
        else HasSettedGUI = false;
    }

    /// <summary>
    /// Sets the gui
    /// </summary>
    private void DisplayStats()
    {
        float time = Time.realtimeSinceStartup - StartTime;
        text.text = "Tijd: " + time.ToString("0.00") + " s" + '\n';
        text.text += "Schoten:";

        foreach (var hit in Scenario.GetHits())
        {
            text.text += "\n" + hit.part.ToDescriptionString();
        }

        HasSettedGUI = true;
    }
}