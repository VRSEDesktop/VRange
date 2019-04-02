using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFog : MonoBehaviour {


    [SerializeField] float timeToFade = 3.0f;

    [Header("Gradient Fog")]

    [SerializeField] Gradient LocalGradient;
    [SerializeField] float startDistance = 0.0f;
    [SerializeField] float endDistance = 100.0f;
    [SerializeField] int textureWidth = 32;

    [Header("Height Fog")]

    public Color heightFogColor = Color.grey;
    public float heightFogThickness = 1.15f;
    public float heightFogFalloff = 0.1f;
    public float heightFogBaseHeight = -40.0f;


    private ValveFog vFog; 

    private void Start()
    {
        vFog = FindObjectOfType<ValveFog>();
    }



    public void FlipFog()
    {
        
    }


    public void FadeFogLUT()
    {
        vFog.FadeFogArray(LocalGradient, timeToFade);
    }

    public void FadeFogLUTtoDefaul()
    {
        vFog.FadeFogToDefault(1.0f);
        
    }

    public void UpdateConstants()
    {
        vFog.UpdateConstants(heightFogColor, LocalGradient,endDistance,startDistance);
    }







}
