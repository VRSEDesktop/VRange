using DG.Tweening;
using System.Collections;
using UnityEngine;
/// <summary>
/// Script used for visualising the transition from the shooting range to the city
/// </summary>
public class Matrix_Effect : MonoBehaviour
{
    public Animator animator;
    public AudioSource collapseSound;
    public GameObject plane;
    public GameObject cameraRig;

    /// <summary>
    /// Start point of the excersises for the player
    /// </summary>
    public GameObject startPoint;

    void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("Fall down");
        collapseSound.Play();
        MoveShootingRange();

        Destroy(gameObject, 7f);
    }

    /// <summary>
    /// Moves the shooting range with player and stuff to start position of the excersise
    /// </summary>
    private void MoveShootingRange()
    {
        DOTween.To(() => transform.position, pos => transform.position = pos, startPoint.transform.position, 5);
        DOTween.To(() => plane.transform.position, pos => plane.transform.position = pos, startPoint.transform.position - new Vector3(0, -0.1f, 0), 5);
        DOTween.To(() => cameraRig.transform.position, pos => cameraRig.transform.position = pos, startPoint.transform.position + new Vector3(0, 0.01f, 0), 5);
    }
}
