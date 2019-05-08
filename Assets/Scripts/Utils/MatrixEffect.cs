using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// Script used for visualising the transition from the shooting range to the city
/// </summary>
public class MatrixEffect : MonoBehaviour
{
    public Animator Animator;
    public AudioSource CollapseSound;
    public GameObject Plane, CameraRig;

    /// <summary>
    /// Start point of the excersises for the player
    /// </summary>
    public GameObject startPoint;

    public void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(5f);
        Animator.Play("Fall down");
        CollapseSound.Play();
        MoveShootingRange();

        Destroy(gameObject, 7f);
    }

    /// <summary>
    /// Moves the shooting range with player and stuff to start position of the excersise
    /// </summary>
    private void MoveShootingRange()
    {
        Vector3 planePos = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y - 0.1f, startPoint.transform.position.z);

        DOTween.To(() => transform.position, pos => transform.position = pos, startPoint.transform.position, 5);
        DOTween.To(() => Plane.transform.position, pos => Plane.transform.position = pos, planePos, 5);
        DOTween.To(() => CameraRig.transform.position, pos => CameraRig.transform.position = pos, startPoint.transform.position, 5);
    }
}
