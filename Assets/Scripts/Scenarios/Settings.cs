using UnityEngine;

[CreateAssetMenu(menuName = "Exercise/Settings")]
public class Settings : ScriptableObject
{
    public bool DrawLines = true;
	/// <summary>
	/// Is a normal gun used or a Vive controller
	/// </summary>
	public bool NormalGun = false;
}
