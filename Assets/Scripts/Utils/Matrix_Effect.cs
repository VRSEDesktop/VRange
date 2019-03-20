using DG.Tweening;
using System.Collections;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Script used for visualising the transition from the shooting range to the city
/// </summary>
public class Matrix_Effect : MonoBehaviour
{
    public Animator animator;
    public AudioSource collapseSound;
    public GameObject plane;
    public GameObject cameraRig;

    void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("Fall down");
        collapseSound.Play();

        DOTween.To(() => transform.position, pos => transform.position = pos, new Vector3(0, 0, 0), 5);
        DOTween.To(() => plane.transform.position, pos => plane.transform.position = pos, new Vector3(0, 0.01f, 0), 5);
        DOTween.To(() => cameraRig.transform.position, pos => cameraRig.transform.position = pos, new Vector3(0, 0, 0), 5);

        Destroy(gameObject, 7f);
    }
}
