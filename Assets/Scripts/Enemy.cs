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
		HitboxType partHit = HitboxType.NONE;
        foreach (Hitbox hitbox in hitboxes)
		{
			if(hitbox.mesh == bulletHit.raycastHit.collider)
			{
				partHit = hitbox.type;
				break;
			}
		}

        switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HEAD:
                Debug.Log("HIT HEAD");
            break;

            case HitboxType.TORSO:
                Debug.Log("HIT TORSO");
            break;
        }
    }
}
