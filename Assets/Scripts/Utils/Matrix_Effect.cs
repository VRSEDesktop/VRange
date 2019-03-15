using System.Collections;
using UnityEngine;

/// <summary>
/// Script used for visualising the transition from the shooting range to the city
/// </summary>
public class Matrix_Effect : MonoBehaviour
{
    public Animator animator;
    public AudioSource collapseSound;

    void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("Fall down");
        collapseSound.Play();

        GameObject.Find("City").SendMessage("EmergeFromGround");

        Destroy(gameObject, 7f);
    }
}
