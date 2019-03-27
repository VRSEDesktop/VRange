using UnityEngine;

public class HideCovers : MonoBehaviour
{
    public void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers) renderer.enabled = false;
    }
}