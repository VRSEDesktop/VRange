using UnityEngine;

public class NextSceneStateButton : Button
{
    public override void Activate()
    {
        Scenario.Clear();
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().NextStep();
    }
}
