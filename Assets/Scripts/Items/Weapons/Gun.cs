using UnityEngine;

public class Gun : Weapon, IReloadable
{
    /// <summary>
    /// Used for drawing bullet's path
    /// </summary>
    public bool drawLines;

    public int magCapacity;
    public int currentAmmo;

    public AudioSource shotSound;
    public AudioSource triggerSound;

    public Animator anim;
    /// <summary>
    /// The place where bullets spawn
    /// </summary>
    public Transform barrelExit;
    public GameObject bulletHole;
    /// <summary>
    /// Prefab for the bulletline, should just be a square.
    /// </summary>
    public GameObject BulletLine;

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

        if (!CanShoot())
        {
            ScenarioLogs.logs.Add(new LoggedShot(this, true));
            return false;
        }

        shotSound.Play();
        DetectHit();

        currentAmmo--;

        GetComponentInChildren<ParticleSystem>().Play();
        anim.Play("Fire");

        ScenarioLogs.logs.Add(new LoggedShot(this, true));
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

            if (!hit.collider.gameObject.GetComponent<Enemy>()) SpawnBulletHole(hit);

            IHitable target = hit.transform.GetComponentInParent<IHitable>();
            if (target == null)
            {
                if (drawLines) BulletLines.SpawnLine(BulletLine, barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);
                return;
            }

            HitType type = target.OnHit(this, hit);

            if (drawLines)
            {
                Color linecolor = Color.red;
                switch (type)
                {
                    case HitType.MISS:
                        linecolor = Color.red;
                        break;
                    case HitType.RIGHT:
                        linecolor = Color.green;
                        break;
                    case HitType.UNWANTED:
                        linecolor = Color.magenta;
                        break;
                }
                BulletLines.SpawnLine(BulletLine, barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, linecolor);
            }

        }
        else
        {
            if (drawLines) BulletLines.SpawnLine(BulletLine, barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);
        }
    }

    private void SpawnBulletHole(RaycastHit hit)
    {
        if (hit.collider.GetComponentInParent<Enemy>()) return;

        Vector3 offset = transform.rotation * -Vector3.forward;
        offset.Scale(new Vector3(0.005f, 0.005f, 0.005f));
        Vector3 position = hit.point - offset;

        GameObject hole = Instantiate(bulletHole, hit.collider.gameObject.transform);
        hole.transform.rotation = Quaternion.LookRotation(hit.normal);
        hole.transform.position = position;
        hole.transform.localScale = new Vector3(0.7f / hole.transform.lossyScale.x, 0.7f / hole.transform.lossyScale.y, 0.7f / hole.transform.lossyScale.z);
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
