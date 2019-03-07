using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    /// <summary>
    /// NPC GameObject
    /// </summary>
    public GameObject NPC;
    /// <summary>
    /// Array of weapon GameObjects suspects can use
    /// </summary>
    public GameObject[] Weapons;
    /// <summary>
    /// Number of normal NPCs
    /// </summary>
    public int Bystanders;
    /// <summary>
    /// Number of suspects
    /// </summary>
    public int Suspects;

    private List<Transform> SpawnPoints = new List<Transform>();
    private List<GameObject> BystanderList = new List<GameObject>();
    private List<GameObject> SuspectList = new List<GameObject>();
    /// <summary>
    /// GameObject to make the Bystander list easier on the eyes in the editor hierarchy.
    /// </summary>
    private GameObject BystanderObject;
    /// <summary>
    /// GameObject to make the Suspect list easier on the eyes in the editor hierarchy.
    /// </summary>
    private GameObject SuspectObject;

    private void Awake()
    {
        BystanderObject = new GameObject("Bystanders");
        SuspectObject = new GameObject("Suspects");

        UpdateSpawnPoints();
    }

    private void Start()
    {
        while (BystanderList.Count < Bystanders)
        {
            SpawnBystander();
        }
        while (SuspectList.Count < Suspects)
        {
            SpawnSuspect();
        }
    }

    /// <summary>
    /// Initialize and spawn a regular NPC
    /// </summary>
    private void SpawnBystander()
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Count);
        GameObject bystander = Instantiate(NPC, BystanderObject.transform);
        bystander.transform.position = SpawnPoints[spawnPoint].position;

        BystanderList.Add(bystander);
    }

    /// <summary>
    /// Initialize and spawn a suspect
    /// </summary>
    private void SpawnSuspect()
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Count);

        GameObject suspect = Instantiate(NPC, SuspectObject.transform);
        suspect.transform.position = SpawnPoints[spawnPoint].position;

        int weaponindex = Random.Range(0, Weapons.Length);
        GameObject weapon = Weapons[weaponindex];

        //Add the gun to the NPC GameObject's script
        NPCController suspectController = suspect.GetComponent<NPCController>();
        suspectController.Weapon = weapon;
        suspectController.StateMachine.ChangeState(AttackPlayer.Instance);

        SuspectList.Add(suspect);
    }

    private void UpdateSpawnPoints()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            SpawnPoints.Add(o.transform);
        }
    }
}