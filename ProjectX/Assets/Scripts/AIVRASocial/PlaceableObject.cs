using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is given to each object to handle it's state changes
public class PlaceableObject : MonoBehaviour
{
    private bool isGhosted;
    public bool validAreaToPlace {get; private set;}
    private bool isColliding = false;

    private MeshCollider meshCollider; //References to object components
    //private Transform transform;
    private Rigidbody rigidbody;
    private Renderer renderer;

    public Material[] mats; //stores defaultMat, blueMat, redMat

    void Update() {
      isColliding = false;
    }

    public void GhostObject(bool ghost) { //Changes status of object (ghosted or unghosted)
      validAreaToPlace = true; //By default, we can place the object. (Objects cannot be inside each other when being spawned)
      meshCollider = this.gameObject.GetComponent<MeshCollider>(); //Get all the components
      //transform = this.transform;
      rigidbody = this.gameObject.GetComponent<Rigidbody>();
      renderer = this.gameObject.GetComponent<Renderer>();
      if (ghost) {
        isGhosted = true;
        renderer.material = mats[2]; //Change the material on the object
        rigidbody.isKinematic = true; //Make the rigidbody kinematic (ignores natural physics)
        meshCollider.convex = true; //Turn the collider to convex
        meshCollider.isTrigger = true; //Make the collider a trigger
      } else {
        isGhosted = false;
        renderer.material = mats[0];
        rigidbody.isKinematic = false;
        meshCollider.convex = true;
        meshCollider.isTrigger = false;
      }
    }

    void OnTriggerEnter(Collider col) {
      if (isGhosted && col.gameObject.layer == 8 && !isColliding) { //PlayerPlacedObjects layer = 8
        isColliding = true; //This stops OnTriggerEnter from being called multiple times (Unity bug)
        renderer.material = mats[1]; //Change the mat to red if they collide with something
        validAreaToPlace = false;
      }
    }

    void OnTriggerExit(Collider col) {
      if (isGhosted && col.gameObject.layer == 8) {
        renderer.material = mats[2]; //Change it back to blue once they get out of the collider
        validAreaToPlace = true;
      }
    }
}
