using UnityEngine;

/// <summary>
/// Script used to adjuct a transform of a controller
/// </summary>
public class ApplyGunRotation : MonoBehaviour
{
	private static Quaternion ControllerRotation = Quaternion.Euler(-45, -180, 0);
	private static Quaternion GunRotation = Quaternion.Euler(0, -88, 90);
	private static Vector3 ControllerOffset = new Vector3(0, -0.05f, -0.05f);
	private static Vector3 GunOffset = new Vector3(0.05f, 0, -0.1f);
	private Vector3 CurrentOffset;

	public Gun Gun;
	private Settings Settings;

	public void Start()
    {
		Settings = GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Settings;

		Apply();
	}

	public void Apply()
	{
		if (CurrentOffset != null) Gun.transform.position -= CurrentOffset;

		if (Settings.NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
	}

	public void ApplyControllerRotation()
    {     
        Gun.transform.rotation = ControllerRotation;
		Gun.transform.position += ControllerOffset;
		CurrentOffset = ControllerOffset;
	}

	public void ApplyRealGunRotation()
    {
		Gun.transform.rotation = GunRotation;
		Gun.transform.position += GunOffset;
		CurrentOffset = GunOffset;
	} 
}
