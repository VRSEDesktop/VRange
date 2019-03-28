using UnityEngine;

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
