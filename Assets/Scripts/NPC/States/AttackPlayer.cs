using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : State<AIController>
{ 
    private static AttackPlayer _instance;
    private ShouldSurrender ShouldSurrender = new ShouldSurrender();
    private ShouldMoveToCover ShouldMoveToCover = new ShouldMoveToCover();

    private float LastShotTime;

    private AttackPlayer()
    {
        if(_instance != null)
            return;

        _instance = this;
    }

    public static AttackPlayer Instance
    {
        get
        {
            if (_instance == null)
                new AttackPlayer();
            return _instance;
        }
    }

    public override void EnterState(AIController owner)
    {
        const float targetDistanceToPlayer = 5f; // desired distance to player

        AISuspectController owner2 = (AISuspectController) owner;
        Vector3 playerPos = owner2.Player.transform.position;
        Vector3 aiPos = owner2.transform.position;

        // Calculate destination in a straight line from the player
        Vector3 offset = new Vector3(playerPos.x - aiPos.x, playerPos.y - aiPos.y, playerPos.z - aiPos.z);
        offset.Normalize();
        offset *= targetDistanceToPlayer;
        Vector3 destination = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        destination -= offset;

        NavMeshPath path = new NavMeshPath();
        owner2.NavAgent.CalculatePath(destination, path);
        if(path.status == NavMeshPathStatus.PathInvalid) // try other destination if this is invalid
        {
            do
            {
                // check random positions around player
                destination.Set(Random.Range(-targetDistanceToPlayer, targetDistanceToPlayer),
                                Random.Range(-targetDistanceToPlayer, targetDistanceToPlayer),
                                Random.Range(-targetDistanceToPlayer, targetDistanceToPlayer));
                destination += playerPos;
                owner2.NavAgent.CalculatePath(destination, path);
            }
            while (path.status == NavMeshPathStatus.PathInvalid);
        }

        //owner2.NavAgent.SetDestination(destination);
    }

    public override void ExitState(AIController owner)
    {
        
    }

    public override void Update(AIController owner)
    {
        //SetWeaponPosition(owner);
        //SetMovement((AISuspectController) owner);
        //SetState(owner);

        //AttackWithGun(owner);
        TakeCover((AISuspectController)owner);
    }

    private void TakeCover(AISuspectController owner)
    {
        IList<GameObject> covers = FindCovers(owner);
        if (covers.Count == 0) return;

        GameObject nearestCover = null;
        float minDistance = 100000;

        foreach(GameObject cover in covers)
        {
            float distance = Vector3.Distance(cover.transform.position, owner.transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                nearestCover = cover;
            }
        }

        owner.NavAgent.SetDestination(nearestCover.transform.position);
    }

    private IList<GameObject> FindCovers(AISuspectController owner)
    {
        IList<GameObject> covers = new List<GameObject>();

        const float maxDistance = 50f;

        GameObject[] allCovers = GameObject.FindGameObjectsWithTag("Cover");
        foreach (GameObject cover in allCovers)
        {
            float distance = Vector3.Distance(cover.transform.position, owner.transform.position);
            if(distance > maxDistance) continue;

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

    private void AttackWithGun(AIController owner)
    {
        if (Time.frameCount % 10 != 0) return;

        AdjustGun((AISuspectController)owner);

        if(!IsPlayerVisible((AISuspectController)owner)) return;

        if(!ShouldShot()) return;

        bool hasShot = owner.Item.Use();

        LastShotTime = Time.time;

        if(!hasShot)
        {
            if (HasSpareMagazines(owner)) ReloadGun(owner);
            else
            {
                // TODO we can change some values such as surrender probability
            }
        }
    }

    private bool ShouldShot()
    {
        const float maxThreshold = 10;
        const float timeShift = 1f;
        float d = Mathf.Sqrt(1.0f);

        float delta = Mathf.Abs(Time.time - LastShotTime);
        if (delta > maxThreshold) delta = 0;

        /// normal/Gaussian distribution
        float probability = Mathf.Exp(-Mathf.Pow(0-timeShift, 2f) / (2f*d*d)) 
            / (d * Mathf.Sqrt(2f*Mathf.PI));

        Debug.Log("Last: " + LastShotTime + " Delta: " + delta+" Prob: " + probability);
        return probability >= Random.Range(0f, 1f);
    }

    // tmp way to adjust gun transform to shoot in the player direction
    private void AdjustGun(AISuspectController owner)
    {        
        Vector3 headPos = owner.transform.position; // change to head pos
        
        owner.Item.transform.position = new Vector3(headPos.x, headPos.y + 1f, headPos.z);
        Vector3 suspectRotation = owner.transform.rotation.eulerAngles;

        //Debug.Log(headPos + " " + owner.Item.transform.position);

        owner.Item.transform.localEulerAngles = new Vector3(suspectRotation.x, suspectRotation.y - 45, suspectRotation.z);

        owner.Item.transform.Rotate(CalculateShotOffset());
    }

    /// <summary>
    /// Calculate new shot direction basing on the target position adding some offset
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateShotOffset()
    {
        const float baseOffset = 5;
        float skill = Random.Range(0f, 1f); // TODO replace with NPC parameter
        float maxAngularOffset = baseOffset * (1 - (Mathf.Pow(10f, skill)-1f)/9f);
        float dX = Random.Range(-maxAngularOffset, maxAngularOffset);
        float dY = Random.Range(-maxAngularOffset, maxAngularOffset);

        return new Vector3(dX, dY, 0);
    }

    /// <summary>
    /// Check if the AI has spare magazines for a gun
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    private bool HasSpareMagazines(AIController owner)
    {
        return ((AISuspectController)owner).SpareMagazines >= 1;
    }

    /// <summary>
    /// Start reloading the gun
    /// </summary>
    /// <param name="owner"></param>
    private void ReloadGun(AIController owner)
    {
        Debug.Log("RELOAD");
        // TODO trigger animation, add time requirement without blocking the AI
        ((AISuspectController)owner).SpareMagazines--;
        ((Gun)owner.Item).Reload();
    }

    public override void OnTriggerStay(AIController owner, Collider other)
    {
        owner.PlayerInSight = IsPlayerVisible((AISuspectController) owner);
    }

    private bool IsPlayerVisible(AISuspectController suspect)
    {
        Vector3 playerHeadPos = suspect.Player.transform.position;
        Vector3 suspectHeadPos = suspect.transform.position;
        Vector3 toPlayer = new Vector3(playerHeadPos.x, playerHeadPos.y, playerHeadPos.z) - suspectHeadPos;

        const float searchRadius = 50f;
        Physics.Raycast(suspectHeadPos, toPlayer.normalized, out RaycastHit hit, searchRadius);
        if(hit.transform == null) return false;

        return hit.transform.Equals(suspect.Player.transform);
    }

    private void CreateShotRepresentation(Vector3 start, Vector3 end, Color color, float duration = 5f)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

        obj.transform.localScale = new Vector3(thickness, thickness, length);
        obj.transform.position = start + ((end - start) / 2);
        obj.transform.LookAt(end);

        obj.GetComponent<MeshRenderer>().material.color = color;
        obj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        obj.SetActive(true);

        GameObject.Destroy(obj, duration);
    }

    public override void OnTriggerExit(AIController owner, Collider other)
    {
        // owner.PlayerInSight = !(other.gameObject == owner.Player);
    }

    /// <summary>
    /// Place the weapon in the NPC's hand.
    /// </summary>
    private void SetWeaponPosition(AIController owner)
    {
        
    }

    /// <summary>
    /// Logic of the NPC's movement, aim and animations.
    /// </summary>
    private void SetMovement(AISuspectController owner)
    {
        float distanceToPlayer = Vector3.Distance(owner.Player.transform.position, owner.transform.position);

        //Reached a destination
        if(owner.NavAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {

            //owner.NavAgent.SamplePathPosition(NavMesh.AllAreas, 0f, out owner.CurrentNavMesh);
        }
    }

    /// <summary>
    /// Logic for changing the state of the NPC's statemachine.
    /// </summary>
    private void SetState(AIController owner)
    {
        //Someting about chances to surrender, dying, etc. in here. Perhaps something with level of fear and level of agression.
        if (ShouldSurrender.Decide(owner))
            // TODO remporary change owner.StateMachine.ChangeState(Surrender.Instance);

        if(ShouldMoveToCover.Decide(owner))
        {
            //Find nearest cover
            //owner.Agent.SetDestination();
        }

        switch (owner.CurrentNavMesh.mask)
        {
            //Walkable
            case 0:
                break;
            //Not Walkable
            case 1:
                break;
            //Jump
            case 2:
                break;
            //User 3
            case 3:
                //owner.StateMachine.ChangeState(TakeCover.Instance);
                break;
        }
    }
}
