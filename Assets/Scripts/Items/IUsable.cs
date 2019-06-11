public interface IUsable
{
    /// <summary>
    /// Use the item
    /// </summary>
    /// <returns>
    /// Returns true if the item was used correctly, false if not
    /// </returns>
    bool Use();
}

/// <summary>
/// Can be used like in exercise states for randomizing drawn weapon
/// </summary>
public enum Item
{
	GUN, PHONE
}