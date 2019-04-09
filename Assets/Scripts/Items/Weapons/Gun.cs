using UnityEngine;

public class Gun : Weapon, IReloadable
{
    /// <summary>
    /// Used for drawing bullet's path
    /// </summary>
    public bool drawLines;

    public int magCapacity;
    private int currentAmmo;

    public AudioSource shotSound;
    public AudioSource triggerSound;

    public Animator anim;
    /// <summary>
    /// The place where bullets spawn
    /// </summary>
    public Transform barrelExit;

    public GameObject bulletLine;
    public GameObject bulletHole;

    public void Start()
    {
        currentAmmo = magCapacity;      
    }

    /// <summary>
    /// Use the gun and return if the gun fired
    /// </summary>
    /// <returns></returns>
    public override bool Use()
    {
        triggerSound.Play();

        if(!CanShoot())
        {
            Scenario.logs.Add(new LoggedShot(this, true));
            return false;
        }

        shotSound.Play();
        DetectHit();

        currentAmmo--;

        GetComponentInChildren<ParticleSystem>().Play();
        anim.Play("Fire");

        Scenario.logs.Add(new LoggedShot(this, true));
        return true;
    }

    /// <summary>
    /// Checks the collison with the bullet ray and handles them
    /// </summary>
    private void DetectHit()
    {
        bool hasHit = Physics.Raycast(barrelExit.transform.position, transform.rotation * -Vector3.forward, out RaycastHit hit);

        if (hasHit)
        {
            Debug.Log("Gun::DetecHit() " + hit.collider.name);

            SpawnBulletHole(hit);

            IHitable target = hit.transform.GetComponentInParent<IHitable>();
            if(target == null)
            {
                if (ShouldDrawLine()) CreateShotRepresentation(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);
                return;
            }

            HitType type = target.OnHit(this, hit);

               switch(type)
                {
                    case HitType.MISS:
                        if(ShouldDrawLine()) CreateShotRepresentation(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);
                        break;
                    case HitType.RIGHT:
                        if (ShouldDrawLine()) CreateShotRepresentation(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.green);
                        break;
                    case HitType.UNWANTED:
                        if (ShouldDrawLine()) CreateShotRepresentation(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.magenta);
                        break;
                }
        }      
        else
        {
            if (ShouldDrawLine()) CreateShotRepresentation(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);
        }
    }

    private void SpawnBulletHole(RaycastHit hit)
    {
        Vector3 offset = transform.rotation * -Vector3.forward;
        offset.Scale(new Vector3(0.005f, 0.005f, 0.005f));
        Vector3 position = hit.point - offset;

        GameObject hole = Instantiate(bulletHole, hit.collider.gameObject.transform);
        hole.transform.rotation = Quaternion.LookRotation(hit.normal);
        hole.transform.position = position;
        hole.transform.localScale = new Vector3(0.7f / hole.transform.lossyScale.x, 0.7f / hole.transform.lossyScale.y, 0.7f / hole.transform.lossyScale.z);
    }

    private bool ShouldDrawLine()
    {
        GameObject exercise = GameObject.FindGameObjectWithTag("Exercise");
        if (exercise && exercise.GetComponent<Settings>())
        {
            return (drawLines && exercise.GetComponent<Settings>().drawLines);
        }
        else return drawLines;
    }

    /// <summary>
    /// Debug function for drawing the bullet trajectory
    /// </summary>
    private void CreateShotRepresentation(Vector3 start, Vector3 end, Color color, float duration = 15f)
    {
        GameObject shotsContainer = GameObject.Find("ShotsRays");
        if (shotsContainer == null) shotsContainer = new GameObject("ShotsRays");

        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.parent = shotsContainer.transform;

        float thickness = 0.005f;
        float length = Vector3.Distance(start, end);

        obj.transform.localScale = new Vector3(thickness, thickness, length);
        obj.transform.position = start + ((end - start) / 2);
        obj.transform.LookAt(end);

        obj.GetComponent<MeshRenderer>().material.color = color;
        obj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        obj.GetComponent<Collider>().enabled = false;

        obj.SetActive(true);
        Destroy(obj, duration);
    }

    /// <summary>
    /// Check if the gun can fire
    /// </summary>
    private bool CanShoot()
    {
        if (!HasAmmo()) return false;

        return true;
    }

    /// <summary>
    /// Reload the gun magazine
    /// </summary>
    public void Reload()
    {
        currentAmmo = magCapacity;
    }

    /// <summary>
    /// Check if the gun has ammo
    /// </summary>
    /// <returns></returns>
    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
}
