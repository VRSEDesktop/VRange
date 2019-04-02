using UnityEngine;

public class GPUInstancingTest : MonoBehaviour
{
    [SerializeField]
     Transform prefab;
    [SerializeField]
     int instances = 5000;
    [SerializeField]
    float radius = 50f;
    [SerializeField]
     bool RandomizeColor;

    void Start()
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();

        
            for (int i = 0; i < instances; i++)
        {
            Transform t = Instantiate(prefab);
            t.localPosition = Random.insideUnitSphere * radius;
            t.SetParent(transform);

            if (RandomizeColor == true)
            {                
            properties.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
            t.GetComponent<MeshRenderer>().SetPropertyBlock(properties);
            }

        }
    }
}