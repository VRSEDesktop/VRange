using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, ISightActivable
{
    public void Activate()
    {
        GameObject.FindGameObjectWithTag("Exercise").GetComponent<Exercise>().Restart();
    }

    public void Draw()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
