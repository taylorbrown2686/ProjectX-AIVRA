using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script can be attached to any object to make it rotate on any axis at any speed
public class RotateObjectContinuously : MonoBehaviour
{
    private float xRot, yRot, zRot;
    public float XRotateSpeed {get => xRot; set => xRot = value;} //Properties for rotation speed
    public float YRotateSpeed {get => yRot; set => yRot = value;}
    public float ZRotateSpeed {get => zRot; set => zRot = value;}

    void Update()
    {
        this.gameObject.transform.Rotate(xRot, yRot, zRot); //Rotate the object 0.5 units a frame
    }
}
