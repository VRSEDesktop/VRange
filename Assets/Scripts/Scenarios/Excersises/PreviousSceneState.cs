using UnityEngine;

public class PreviousSceneState : MonoBehaviour, IHitable
{
    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        Exercise.NextStep();
        return HitType.RIGHT;
    }
}
