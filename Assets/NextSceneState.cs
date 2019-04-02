using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneState : MonoBehaviour, IHitable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        Exercise2.State += 1;
        return HitType.UNWANTED;
    }

}
