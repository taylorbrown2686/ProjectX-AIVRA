using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is given to each object to handle it's state changes
public class PlaceableObject : MonoBehaviour
{
    private bool isGhosted;
    public bool IsGhosted { //Property for editing ghost field
      get {return isGhosted;}
      set {isGhosted = value;}
    }
    private bool validAreaToPlace = false;

    private MeshCollider meshCollider; //References to object components
    private Transform transform;
    private Rigidbody rigidbody;
    private Renderer renderer;

    public Material[] mats = new Material[3]; //stores defaultMat, blueMat, redMat

    void Start() {
      meshCollider = this.GetComponent<MeshCollider>(); //Get all the components
      transform = this.transform;
      rigidbody = this.GetComponent<Rigidbody>();
      renderer = this.GetComponent<Renderer>();
    }

    void Update() {
      if (isGhosted) {
        renderer.material = mats[2]; //Change the material on the object
        rigidbody.isKinematic = true; //Make the rigidbody kinematic (ignores natural physics)
        meshCollider.convex = true; //Turn the collider to convex
        meshCollider.isTrigger = true; //Make the collider a trigger
      } else {
        renderer.material = mats[0];
        rigidbody.isKinematic = false;
        meshCollider.convex = true;
        meshCollider.isTrigger = false;
      }
    }

    void OnTriggerEnter() {
      if (isGhosted) {
        renderer.material = mats[1]; //Change the mat to red if they collide with something
        validAreaToPlace = false;
      }
    }
    void OnCollisionEnter() {
      if (isGhosted) {
        renderer.material = mats[1];
        validAreaToPlace = false;
      }
    }

    void OnTriggerExit() {
      if (isGhosted) {
        renderer.material = mats[2]; //Change it back to blue once they get out of the collider
        validAreaToPlace = true;
      }
    }
}
