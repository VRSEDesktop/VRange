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
    public Weapon[] Weapons;
    /// <summary>
    /// Number of normal NPCs
    /// </summary>
    public int Bystanders;
    /// <summary>
    /// Number of suspects
    /// </summary>
    public int Suspects;

    private Transform[] SpawnPoints;
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
            SpawnNPC(Patrol.Instance, false);
        }
        while (SuspectList.Count < Suspects)
        {
            SpawnNPC(AttackPlayer.Instance, true);
        }
    }

    private void SpawnNPC(State<NPCController> startingState, bool isSuspect)
    {
        int spawnPoint = Random.Range(0, SpawnPoints.Length);
        GameObject npc = Instantiate(NPC, isSuspect ? SuspectObject.transform : BystanderObject.transform);
        npc.transform.position = SpawnPoints[spawnPoint].position;

        NPCController npcController = npc.GetComponent<NPCController>();
        npcController.StateMachine.ChangeState(startingState);
        if(isSuspect)
        {
            int weaponindex = Random.Range(0, Weapons.Length);
            Weapon weapon = Instantiate(Weapons[weaponindex], npc.transform);
            weapon.transform.parent = npc.transform;
            weapon.transform.SetAsFirstSibling();

            //Add the gun to the NPC GameObject's script
            npcController.Item = weapon;
            SuspectList.Add(npc);
        }
        else
        {
            BystanderList.Add(npc);
        }
    }

    private void UpdateSpawnPoints()
    {
        List<Transform> spawnPointList = new List<Transform>();
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Waypoint");
        for (int i = 0; i <  gameObjectArray.Length; ++i )
        {
            spawnPointList.Add(gameObjectArray[i].transform);
        }
        SpawnPoints = spawnPointList.ToArray();
    }
}