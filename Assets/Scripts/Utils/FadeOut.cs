using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public void StartFadeOut(float time)
	{
		IEnumerator routine = DisableLine(time);
		StartCoroutine(routine);
	}

	private IEnumerator DisableLine(float delayinseconds)
	{
		Renderer renderer = gameObject.GetComponent<Renderer>();
		for (float f = delayinseconds; f >= 0; f -= 0.1f)
		{
			Color newcolor = renderer.material.color;
			newcolor.a = f;
			renderer.material.color = newcolor;
			yield return new WaitForSeconds(.1f);
		}
		gameObject.SetActive(false);

		//Reset the alpha, since material.color.a isn't accessible overwrite color with a new color object.
		Color resetcolor = renderer.material.color;
		resetcolor.a = 1;
		renderer.material.color = resetcolor;
	}
}
