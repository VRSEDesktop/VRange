using UnityEngine;

public delegate void PropertyChanged();

[CreateAssetMenu(menuName = "Exercise/Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private bool _drawLines = true;
	public bool NormalGun = false;
	public event PropertyChanged SettingsChanged;

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
