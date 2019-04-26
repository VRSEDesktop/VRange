using UnityEngine;

/// <summary>
/// Script used to adjuct a transform of a controller
/// </summary>
public class ApplyGunRotation : MonoBehaviour
{
    public Gun Gun;
	private Settings Settings;

    public void Start()
    {
		Settings = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Settings;

		Switch();
	}

	public void Switch()
	{
		if (Settings.NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
	}

    public void ApplyControllerRotation()
    {     
        Gun.transform.rotation = Quaternion.Euler(-45, -180, 0);
        Gun.transform.position = new Vector3(0, -0.05f, -0.05f);
    }

	public void ApplyRealGunRotation()
    {
        Gun.transform.rotation = Quaternion.Euler(0, -88, 90);
        Gun.transform.position = new Vector3(0.05f, 0, -0.1f);
    } 
}
