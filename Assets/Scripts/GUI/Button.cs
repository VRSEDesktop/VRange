using UnityEngine;

public abstract class Button : MonoBehaviour, ISightActivable
{
    private Animator loadingAnimation;
    public string type;
    public bool isActivated;

    public virtual void Start()
    {
        loadingAnimation = GetComponentInChildren<Animator>();
        //loadingAnimation.SetBool("Loading", false);
    }

    public abstract void Activate();

    public void OnHoverStart()
    {
        //loadingAnimation.SetBool("Loading", true);
    }

    public void OnHoverEnd()
    {
        //loadingAnimation.SetBool("Loading", false);
    }
}