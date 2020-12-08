using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScale : MonoBehaviour
{

    float x=1, z=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(x, (x+z)/2, z);
    }

    public void SetScale(float x, float z)
    {
        this.x = x;
        this.z = z;
    }
}
