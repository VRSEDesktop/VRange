using UnityEngine;

public abstract class Weapon : MonoBehaviour, IUsable
{
    public abstract bool Use();
}
