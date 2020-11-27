using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private int value;
   // private Rigidbody rigidbody;

    public int Value {get => value;}
    GameObject gameZone;

    private void Start()
    {
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        GameObject plane = gameZone.transform.GetChild(0).gameObject;
      //  var emptyObject = new GameObject();
        transform.SetParent(plane.transform);
     //   emptyObject.transform.parent = plane.transform;
     //   transform.parent = emptyObject.transform;
    }

    public void ReadDie() {
      Vector3 angles = this.transform.rotation.eulerAngles;
      if (Vector3.Dot (transform.forward, Vector3.up) > 0.95f)
            value = 2;
      if (Vector3.Dot (-transform.forward, Vector3.up) > 0.95f)
            value = 5;
      if (Vector3.Dot (transform.up, Vector3.up) > 0.95f)
            value = 6;
      if (Vector3.Dot (-transform.up, Vector3.up) > 0.95f)
            value = 1;
      if (Vector3.Dot (transform.right, Vector3.up) > 0.95f)
            value = 4;
      if (Vector3.Dot (-transform.right, Vector3.up) > 0.95f)
            value = 3;
    }
}
