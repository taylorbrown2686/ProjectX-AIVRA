using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is the base class for the Move, Scale, and Rotate scripts
public class AxesBase : MonoBehaviour
{
    private string activeAxis;

    public string ActiveAxis { //'Set' is done here, so we don't need that attribute
      get {return activeAxis;}
    }

    public TempDirectionChange dirChange;

    void Start() {
      dirChange = GameObject.Find("_MAINCONTROLLER").GetComponent<TempDirectionChange>();
    }

    protected void Update() {
      if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) { //If beginning of touch...
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //Get touch position
        RaycastHit hit;
        int layermask = LayerMask.GetMask("Axis");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)) {
          activeAxis = hit.collider.gameObject.name;
          if (activeAxis.Contains("Tip")) {
            activeAxis = GameObject.Find(activeAxis).transform.parent.name;
          }
        }
      }
    }
}
