using UnityEngine;
using DG.Tweening;

/// <summary>
/// Small script for making the city emerge from the ground
/// </summary>
public class CityMatrixEffect : MonoBehaviour
{
    public void EmergeFromGround()
    {
        DOTween.To(() => transform.position, pos => transform.position = pos, new Vector3(0, 0, 0), 5);
    }
}
