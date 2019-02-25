using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Hitable
{
    [System.Serializable]
    public struct Hitbox
    {
        public Collider mesh;
        public HitboxType type;
    }

    public Hitbox[] hitboxes;

    public void OnHit(BulletHit bulletHit)
    {
        //HitboxType
        Debug.Log(((MeshCollider)(bulletHit.raycastHit.collider)).sharedMesh.name);
    }

    public void OnAwake()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("Movement");
    }
}
