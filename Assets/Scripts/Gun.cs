using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool debugMode = false;

    public int magCapacity;
    private int currentAmmo;

    public AudioSource shotSound;
    public AudioSource trigerSound;

    public Transform barrelExit;

    public void Start()
    {
        currentAmmo = magCapacity;
    }

    public void Shoot()
    {
        trigerSound.Play();

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
        RaycastHit hit;

        if(debugMode) DrawLine(barrelExit.transform.position, barrelExit.transform.position + transform.rotation * -Vector3.forward * 10, Color.red);

        if(Physics.Raycast(barrelExit.transform.position, transform.rotation * -Vector3.forward, out hit))
        {
            Debug.Log("Hit: " + hit.transform.name);
            hit.transform.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }           
    }

    /// <summary>
    /// Debug function for drawing the bullet trajectory
    /// </summary>
    private void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.5f)
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
    /// Can the gun fire, depends on available bullets and maybe other factors
    /// </summary>
    private bool CanShoot()
    {
        if (currentAmmo <= 0) return false;

        return true;
    }
}
