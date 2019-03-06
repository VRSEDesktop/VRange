using UnityEngine;

public class Civilian : MonoBehaviour, IHitable
{
	public void OnHit(BulletHit bulletHit)
	{
		// if you hit a civilian you should lose
	}
		
}

