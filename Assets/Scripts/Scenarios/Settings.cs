using UnityEngine;

public delegate void PropertryChanged();

[CreateAssetMenu(menuName = "Exercise/Settings")]
public class Settings : ScriptableObject
{
    private bool _drawLines = true;
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
