using UnityEngine;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// Used for drawing bullet's path
    /// </summary>
    public bool debugMode = false;

    public int magCapacity;
    public int shotsFired;
    private int currentAmmo;

    public AudioSource shotSound;
    public AudioSource triggerSound;

    /// <summary>
    /// The place where bullets spawn
    /// </summary>
    public Transform barrelExit;

    public void Start()
    {
        currentAmmo = magCapacity;
    }

    public void Shoot()
    {
        triggerSound.Play();

        if (!CanShoot()) return;

        shotSound.Play();
        currentAmmo--;

        GetComponentInChildren<ParticleSystem>().Play();

        DetectHit();
    }

    /// <summary>
    /// Checks the collison with the bullet ray and handles them
    /// </summary>
    private void DetectHit()
    {
        if (debugMode) DrawLine(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);

        if (Physics.Raycast(barrelExit.transform.position, transform.rotation * -Vector3.forward, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name);

            IHitable target = hit.transform.GetComponentInParent<IHitable>();
            target?.OnHit(new BulletHit(this, hit));
        }           
    }

    /// <summary>
    /// Debug function for drawing the bullet trajectory
    /// </summary>
    private void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 10f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    /// <summary>
    /// Can the gun fire, depends on available bullets and other factors
    /// </summary>
    private bool CanShoot()
    {
        if (currentAmmo <= 0) return false;

        return true;
    }

    public void Reload()
    {
        currentAmmo = magCapacity;
    }
}
