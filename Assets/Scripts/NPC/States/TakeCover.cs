using System.Collections.Generic;
using UnityEngine;

class TakeCover : State<AIController>
{
    private static TakeCover _instance;

    public TakeCover()
    {
        if (_instance != null)
            return;

        _instance = this;
    }

    public static TakeCover Instance
    {
        get
        {
            if (_instance == null)
                new TakeCover();
            return _instance;
        }
    }

    public override void EnterState(AIController owner)
    {
        IList<GameObject> covers = FindCovers((AISuspectController)owner);
        if (covers.Count == 0) return;

        GameObject nearestCover = null;
        float minDistance = 100000;

        foreach (GameObject cover in covers)
        {
            float distance = Vector3.Distance(cover.transform.position, owner.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCover = cover;
            }
        }

        owner.NavAgent.SetDestination(nearestCover.transform.position);
    }

    public override void ExitState(AIController owner)
    {

    }

    public override void OnTriggerExit(AIController owner, Collider other)
    {

    }

    public override void OnTriggerStay(AIController owner, Collider other)
    {

    }

    public override void Update(AIController owner)
    {
        const float minDistanceToCover = 2f;
        if (owner.NavAgent.remainingDistance < minDistanceToCover)
        {
            owner.StateMachine.ChangeState(AttackPlayer.Instance);
        }
    }

    private IList<GameObject> FindCovers(AISuspectController owner)
    {
        IList<GameObject> covers = new List<GameObject>();

        const float maxDistance = 30f;

        GameObject[] allCovers = GameObject.FindGameObjectsWithTag("Cover");
        foreach (GameObject cover in allCovers)
        {
            float distance = Vector3.Distance(cover.transform.position, owner.transform.position);
            if (distance > maxDistance) continue;

            Vector3 playerPos = owner.Player.transform.position;
            Vector3 suspectPos = owner.transform.position;
            Vector3 dirToPlayer = new Vector3(playerPos.x - suspectPos.x, playerPos.y - suspectPos.y, playerPos.z - suspectPos.z);
            dirToPlayer.Normalize();

            float dot = Vector3.Dot(cover.transform.forward, dirToPlayer);
            if (dot < 0.7) continue;

            covers.Add(cover);
        }

        return covers;
    }
}
