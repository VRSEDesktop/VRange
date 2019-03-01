using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix_Effect : MonoBehaviour
{
    public List<Rigidbody> ceiling;
    public List<Rigidbody> walls;
    public List<Rigidbody> floor;

    public float time;
    public bool finished = false;

    void Start()
    {
        if (!finished)
        {
            StartCoroutine(WallfallDown());
        }
    }

    IEnumerator WallfallDown()
    {
        yield return new WaitForSeconds(3f);
        finished = true;

        for (int i = 0; i < floor.Count; i++)
        {
            yield return new WaitForSeconds(time);
            floor[i].useGravity = true;
        }

        for (int i = 0; i < walls.Count; i++)
        {
            yield return new WaitForSeconds(time);
            walls[i].useGravity = true;
            float delay = Random.Range(0, 0.5f);
            walls[i].GetComponent<AudioSource>().PlayDelayed(delay);
            walls[i].AddRelativeTorque(new Vector3(0, 0, 10000));
            walls[i].useGravity = true;
        }

        for (int i = 0; i < ceiling.Count; i++)
        {
            yield return new WaitForSeconds(time);
            ceiling[i].AddForce(new Vector3(0, ceiling[i].mass * 1000, 0));
            ceiling[i].AddRelativeTorque(new Vector3(10000, 0, 0));
        }
    }
}
