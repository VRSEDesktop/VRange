using UnityEngine;

/// <summary>
/// Used to hide arrows which represent covers for AI
/// </summary>
public class HideCovers : MonoBehaviour
{
    public void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers) renderer.enabled = false;
    }
}