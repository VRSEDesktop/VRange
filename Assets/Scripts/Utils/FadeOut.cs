using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public void StartFadeOut(float delay)
	{
		IEnumerator routine = DisableLine(delay);
		if(gameObject.activeSelf)
			StartCoroutine(routine);
	}

	private IEnumerator DisableLine(float delay)
	{
		yield return new WaitForSeconds(delay);
		gameObject.SetActive(false);
	}
}
