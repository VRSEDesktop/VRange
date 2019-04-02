using UnityEngine;

public class PreviousSceneState : MonoBehaviour, IHitable
{
    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().NextStep();
        return HitType.RIGHT;
    }
}
