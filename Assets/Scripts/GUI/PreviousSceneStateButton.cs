using UnityEngine;

public class PreviousSceneStateButton : Button
{
    public override void Activate()
    {
        Scenario.Clear();
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().PreviousStep();
    }
}
