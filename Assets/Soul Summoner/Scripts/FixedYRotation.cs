using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedYRotation : MonoBehaviour
{
    Quaternion iniRot;

    private void Start()
    {
        iniRot = transform.rotation;
    }
    
        
    
    private void LateUpdate()
    {
        transform.rotation = iniRot;
    }
}
