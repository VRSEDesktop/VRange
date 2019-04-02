using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(ValveRealtimeLight) ) ]
public class OffsetDirCookieOvertime : MonoBehaviour {


    public Vector2 OffsetSpeed;
    ValveRealtimeLight vL;

    void Awake()
    {
        vL = GetComponent<ValveRealtimeLight>();
    }


    void Update()
    {

        vL.DirectionalCookieOffset = ((Time.time * OffsetSpeed));

    }


}
