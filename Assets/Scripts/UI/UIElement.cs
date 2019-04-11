using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    public string Name;

    public virtual void SetActive()
    {
        UI.ActivateButton(Name);
    }

    public virtual void SetInactive()
    {
        UI.DeactivateButton(Name);
    }

    public virtual void OnDisable()
    {
        SetInactive();
    }
}