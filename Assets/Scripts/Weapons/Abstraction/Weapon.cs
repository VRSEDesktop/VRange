using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    public float Range;
    public abstract void PerformAttack();
}
