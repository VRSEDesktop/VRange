using UnityEngine;

public abstract class ExcersiseState : MonoBehaviour
{
    public virtual void OnStart()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(true);
        }
    }

    public abstract void OnUpdate();

    public virtual void OnExit()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
    }
}
