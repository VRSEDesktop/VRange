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
    /// List of GameObjects suspects can use
    /// </summary>
    public GameObject[] Weapons;
    /// <summary>
    /// Number of normal NPCs
    /// </summary>
    public int Bystanders;
    /// <summary>
    /// Number of agressors
    /// </summary>
    public int Suspects;

    private List<Transform> SpawnPoints = new List<Transform>();
    private List<GameObject> BystanderList = new List<GameObject>();
    private List<GameObject> SuspectList = new List<GameObject>();

    private void Awake()
    {
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

    private void SpawnBystander()
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Count);
        GameObject bystander = Instantiate(NPC, SpawnPoints[spawnPoint]);
        BystanderList.Add(bystander);
    }

    private void SpawnSuspect()
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Count);
        GameObject suspect = Instantiate(NPC, SpawnPoints[spawnPoint]);

        int weaponindex = Random.Range(0, Weapons.Length);
        GameObject weapon = Weapons[weaponindex];

        //Add the gun to the prefab's script
        NPCController suspectController = suspect.GetComponent<NPCController>();
        suspectController.Weapon = weapon;

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