using UnityEngine;

public delegate void PropertryChanged();

[CreateAssetMenu(menuName = "Exercise/Settings")]
public class Settings : ScriptableObject
{
	/// <summary>
	/// Is a normal gun used or a Vive controller
	/// </summary>
	public bool NormalGun = false;
    [SerializeField] private bool _drawLines = true;
	public event PropertryChanged SettingsChanged;

	public bool DrawLines
	{
		get { return _drawLines; }
		set
		{
			if(value != _drawLines)
			{
				_drawLines = value;
				SettingsChanged?.Invoke();
			}
		}
	}
}
