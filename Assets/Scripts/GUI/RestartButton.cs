using UnityEngine;

public class RestartButton : Button
{
    public override void Activate()
    {
        Scenario.Clear();
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Restart();

        Gun[] guns = FindObjectsOfType<Gun>();
        foreach (Gun gun in guns) gun.Reload();

        GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("Bullet Hole");
        foreach (GameObject obj in bulletHoles) Destroy(obj);
    }
}
