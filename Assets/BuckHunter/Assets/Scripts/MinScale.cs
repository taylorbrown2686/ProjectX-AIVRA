using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinScale : MonoBehaviour
{
    // Start is called before the first frame update

    public float scale;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < scale)
            transform.localScale = new Vector3(scale, scale, scale);
    }
}
