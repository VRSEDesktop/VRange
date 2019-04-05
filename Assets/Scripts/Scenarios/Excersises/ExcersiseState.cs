using TMPro;
using UnityEngine;

public abstract class ExcersiseState : MonoBehaviour
{
    [HideInInspector]
    public Gun rightGun;
    [HideInInspector]
    public TextMeshPro text;
    public bool HasSettedGUI { get; set; }

    protected float startTime;

    public virtual void OnStart()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(true);
        }

        rightGun = GameObject.FindGameObjectWithTag("RightGun").GetComponentInChildren<Gun>();
        text = GameObject.FindGameObjectWithTag("ShootingStats").GetComponentInChildren<TextMeshPro>();

        startTime = Time.realtimeSinceStartup;
    }

    public abstract void OnUpdate();

    public virtual void Restart()
    {     
        Scenario.Clear();

        startTime = Time.realtimeSinceStartup;
        text.text = "";
        HasSettedGUI = false;
    }

    public virtual void OnExit()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if the stats needs to be changed
    /// </summary>
    public void UpdateGUI()
    {
        if (!rightGun.HasAmmo())
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
        float time = Time.realtimeSinceStartup - startTime;
        text.text = "Tijd: " + time.ToString("0.00") + " s" + '\n';
        text.text += "Schoten:";

        foreach (var hit in Scenario.GetHits())
        {
            text.text += "\n" + hit.part.ToString();
        }

        HasSettedGUI = true;
    }
}