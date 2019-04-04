using UnityEngine;

public abstract class ExcersiseState : MonoBehaviour
{
    [HideInInspector]
    public Gun rightGun;
    [HideInInspector]
    public TextMesh text;
    public bool HasSettedGUI { get; set; }

    private float startTime;

    public virtual void OnStart()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(true);
        }

        rightGun = GameObject.FindGameObjectWithTag("RightGun").GetComponentInChildren<Gun>();
        text = GameObject.FindGameObjectWithTag("ShootingStats").GetComponentInChildren<TextMesh>();

        startTime = Time.realtimeSinceStartup;
    }

    public abstract void OnUpdate();

    public abstract void Restart();

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
                Scenario.Clear();
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
        text.text = "Time: " + time;
        text.text += "\nShooting State:";

        foreach (var hit in Scenario.GetHits())
        {
            text.text += "\n" + hit.part.ToString();
        }

        HasSettedGUI = true;
    }


}
