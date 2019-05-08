using UnityEngine;

public class StateStreet : ExcersiseState
{
    public override void OnStart()
    {
        base.OnStart();
        Exercise.ShootingRange.gameObject.SetActive(false);
        Exercise.City.gameObject.SetActive(true);

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject button in buttons) button.SetActive(false);
    }

    public override void OnUpdate()
    {
    
    }
}
