using UnityEngine;

/// <summary>
/// Script used to adjuct a transform of a controller
/// </summary>
public class ApplyGunRotation : MonoBehaviour
{
	private readonly Quaternion ControllerRotation = Quaternion.Euler(-45, -180, 0);
	private readonly Quaternion GunRotation = Quaternion.Euler(0, -88, 90);
	private readonly Vector3 ControllerOffset = new Vector3(0, -0.05f, -0.05f);
	private readonly Vector3 GunOffset = new Vector3(0.05f, 0, -0.1f);

	public Gun Gun;
	private bool NormalGun = false;

	public void Start()
    {
		if (NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
	}

	public void Update()
	{
		if (UI.GetButtonActivated("Toggle Controller"))
			Toggle();
	}

	public void Toggle()
	{
		Debug.Log("ApplyRotation()");

		if (!NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
		NormalGun = !NormalGun;
	}

	public void ApplyControllerRotation()
    {     
        Gun.transform.localRotation = ControllerRotation;
		Gun.transform.localPosition = ControllerOffset;
	}

	public void ApplyRealGunRotation()
    {
		Gun.transform.localRotation = GunRotation;
		Gun.transform.localPosition = GunOffset;
	} 
}
