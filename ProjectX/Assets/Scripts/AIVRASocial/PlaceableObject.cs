using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private bool isGhosted;
    public bool IsGhosted {
      get {return isGhosted;}
      set {isGhosted = value;}
    }
    private bool validAreaToPlace = false;

    private MeshCollider meshCollider;
    private Transform transform;
    private Rigidbody rigidbody;
    private Renderer renderer;

    public Material[] mats = new Material[3]; //stores defaultMat, blueMat, redMat

    void Start() {
      meshCollider = this.GetComponent<MeshCollider>();
      transform = this.transform;
      rigidbody = this.GetComponent<Rigidbody>();
      renderer = this.GetComponent<Renderer>();
    }

    void Update() {
      if (isGhosted) {
        renderer.material = mats[2];
        rigidbody.isKinematic = true;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
      } else {
        renderer.material = mats[0];
        rigidbody.isKinematic = false;
        meshCollider.convex = true;
        meshCollider.isTrigger = false;
      }
    }

    void OnTriggerEnter() {
      if (isGhosted) {
        renderer.material = mats[1];
        validAreaToPlace = false;
      }
    }

    void OnTriggerExit() {
      if (isGhosted) {
        renderer.material = mats[2];
        validAreaToPlace = true;
      }
    }
}
