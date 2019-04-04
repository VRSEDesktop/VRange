using UnityEngine;

public class RestartButton : MonoBehaviour, ISightActivable
{
    public void Activate()
    {
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Restart();
    }

    public void OnHoverEnd()
    {
       
    }

    public void OnHoverStart()
    {
       
    }
}
