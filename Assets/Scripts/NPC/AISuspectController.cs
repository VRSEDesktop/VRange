using UnityEngine;

public class AISuspectController : AIController
{
    /// <summary>
    /// The level of agression.
    /// </summary>
    public float LevelOfAgression;
    /// <summary>
    /// The strategic competence of the NPC.
    /// </summary>
    public float LevelOfTactics;
    /// <summary>
    /// Additional magazines for the gun
    /// </summary>
    public int SpareMagazines;
    [HideInInspector] public Player Player;

    protected new void OnEnable()
    {
        base.OnEnable();

        LevelOfAgression = Random.Range(0, 100);
        LevelOfTactics = Random.Range(0, 100);
        SpareMagazines = Random.Range(1, 2);
        Player = GameObject.Find("[CameraRig]").GetComponentInChildren<Player>();
    }
}