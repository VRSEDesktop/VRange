using System;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Abstract class for adding new types of controllers
/// </summary>
[RequireComponent(typeof(VR_Controller))]
public abstract class HandController : MonoBehaviour
{
    protected VR_Controller input;

    public void Start()
    {
        input = GetComponent<VR_Controller>();
    }

    public abstract void HandleInput();
}
