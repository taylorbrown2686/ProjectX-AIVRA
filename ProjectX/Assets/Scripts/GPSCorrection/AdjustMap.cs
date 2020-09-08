using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustMap : MonoBehaviour
{
    public OnlineMaps map; //reference to the map script
    public GameObject mapPlane; //gameobjects associated with map
    public Transform mapDown, mapUp; //transforms for up and down map positions
    public bool mapCanMove = true; //bool to force map to complete movement before beginning again
    public bool mapIsUp = true;

    void Update() {
      if (Input.GetKeyDown(KeyCode.I)) {
        MoveMapOnClick();
      }
    }

    public void MoveMapOnClick() { //button onclick event
      if (mapCanMove) {
        StartCoroutine(MoveMap());
      }
    }

    public void CenterMap() {
      map.latitude = Input.location.lastData.latitude; //set map to current lat/long estimate
      map.longitude = Input.location.lastData.longitude;
    }

    public void LerpTransform(Transform t1, Transform t2, float t) { //take in current and new transform
      t1.localPosition = Vector3.Lerp(t1.localPosition, t2.localPosition, t); //apply it to the map
      t1.localRotation = Quaternion.Lerp(t1.localRotation, t2.localRotation, t);
    }

    public IEnumerator MoveMap() {
      if (mapIsUp) {
        mapCanMove = false; //change to false so map can't move
        for (int i = 0; i <= 100; i++) { //iterate 100 times (smooth)
          LerpTransform(this.transform, mapDown, i / 100f);
          yield return new WaitForSeconds(0.005f); //delay between movement
        }
        mapIsUp = false;
        mapCanMove = true;
      } else {
        mapCanMove = false;
        for (int i = 0; i <= 100; i++) {
          LerpTransform(this.transform, mapUp, i / 100f);
          yield return new WaitForSeconds(0.005f);
        }
        mapIsUp = true;
        mapCanMove = true;
      }
    }
}
