using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameStart : MonoBehaviour
{
    public GameObject placeObjectText;
    public GameObject car;
    public ARRaycastManager raycastManager;

    void Update() {
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) {
          Vector2 touchPosition = touch.position; //Get touch position
          List<ARRaycastHit> hits = new List<ARRaycastHit>();
          if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
            car.SetActive(true);
            car.transform.position = hits[0].pose.position + new Vector3(0, 0.5f, 0);
            Destroy(placeObjectText);
            Destroy(this);
          }
        }
      }
    }
}
