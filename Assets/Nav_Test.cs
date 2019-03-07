using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Nav_Test : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform pointA;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(pointA.position);
        GetComponent<Animator>().SetFloat("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
