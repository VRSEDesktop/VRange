using System.Collections;
using UnityEngine;

public class Matrix_Effect : MonoBehaviour
{
    public Animator animator;
    public AudioSource collapseSound;

    void Start()
    {
        StartCoroutine(WallfallDown());
    }

    private IEnumerator WallfallDown()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("Fall down");
        collapseSound.Play();

        GameObject.Find("City").SendMessage("EmergeFromGround");

        Destroy(gameObject, 7f);
    }
}
