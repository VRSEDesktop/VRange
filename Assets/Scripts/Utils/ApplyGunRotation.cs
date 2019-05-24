using UnityEngine;

/// <summary>
/// Script used to adjuct a transform of a controller
/// </summary>
[RequireComponent(typeof(GunController))]
public class ApplyGunRotation : MonoBehaviour
{
	private readonly Quaternion ControllerRotation = Quaternion.Euler(120, 180, 180);
	private readonly Quaternion GunRotation = Quaternion.Euler(180, -88, 90);
	private readonly Vector3 ControllerOffset = new Vector3(0, -0.05f, -0.05f);
	private readonly Vector3 GunOffset = new Vector3(0.05f, 0, -0.1f);

	private Gun Gun;

	public Settings Settings;

	public void Start()
    {
		Gun = GetComponent<GunController>().gun;

		if (Settings.NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
	}

	public void Toggle()
	{
		if (!Settings.NormalGun) ApplyRealGunRotation();
		else ApplyControllerRotation();
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
