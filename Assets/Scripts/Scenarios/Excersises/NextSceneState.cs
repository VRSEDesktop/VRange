using UnityEngine;

public class NextSceneState : MonoBehaviour, IHitable
{
    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().NextStep();
        return HitType.RIGHT;
    }
}
