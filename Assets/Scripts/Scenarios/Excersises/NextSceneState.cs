using UnityEngine;

public class NextSceneState : MonoBehaviour, IHitable
{
    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        Exercise.PreviousStep();
        return HitType.RIGHT;
    }
}
