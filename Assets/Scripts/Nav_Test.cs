using UnityEngine;
using UnityEngine.AI;

public class Nav_Test : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform pointA;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(pointA.position);
        GetComponent<Animator>().SetFloat("Speed", 1);
    }
}
