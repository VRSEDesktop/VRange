using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject NPC;
    public int MaxNPCs;

    private List<Transform> SpawnPoints = new List<Transform>();
    private List<GameObject> NPCList = new List<GameObject>();

    private void Awake()
    {
        UpdateSpawnPoints();
    }

    private void Start()
    {
        while (NPCList.Count < MaxNPCs)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Count);
        GameObject npc = Instantiate(NPC, SpawnPoints[spawnPoint]);
        NPCList.Add(npc);
    }

    private void UpdateSpawnPoints()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            SpawnPoints.Add(o.transform);
        }
    }
}