using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//This script is disgusting in terms of the approach taken to implement this feature.
//This is the only way to do it though, considering ARRaycasts cannot return a GameObject's name
//You can thank Unity for this mess of a script
public class SpawnObjectAtTouch : MonoBehaviour
{
    //TODO: Implement raycast touch -> name of object touched in AR
    //TODO: Implement movement features
    //TODO: Set up buttons to change movement (Getter/Setter)
    private string currentSetting; //Current setting we are on
    private GameObject currentSelectedObject; //Object we have touched last
    public GameObject[] objectsToSpawn;
    private int objectToSpawnIndex = 0;
    public Transform arso; //ARSessionOrigin

    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Text debugText;

    private void Update() {
      if (!TryGetTouchPosition(out Vector2 touchPosition)) {
        return;
      }

      if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition); //Set the active object via a raycast
        RaycastHit hit;
        int layermask = LayerMask.GetMask("PlayerPlacedObject");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)) {
          currentSelectedObject = hit.collider.transform.parent.gameObject;
        }
      }

      switch (currentSetting) {
        case "Spawn":
          if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
              var hitPose = hits[0].pose;
              Instantiate(objectsToSpawn[objectToSpawnIndex], hitPose.position, hitPose.rotation, arso);
            }
          }
        break;

        case "Move":
          if (Input.touchCount == 1) {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
              var hitPose = hits[0].pose;
              currentSelectedObject.transform.position = hitPose.position;
            }
          }
        break;

        case "Rotate":
          if (Input.touchCount == 2) {

          }
        break;

        case "Scale":
          if (Input.touchCount == 2) {
            Touch[] touches = new Touch[2];
            touches[0] = Input.GetTouch(0); //Get an array of touches since we need 2
            touches[1] = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touches[0].position - touches[0].deltaPosition; //touchZeroPreviousPosition (abbreviated)
            Vector2 touchOnePrevPos = touches[1].position - touches[1].deltaPosition;
            float touchDeltaMag = (touches[0].position - touches[1].position).magnitude;
            float finalScale = touchDeltaMag / 1000; //Value is based on pixels between fingers, so make the scale smaller
            currentSelectedObject.transform.localScale = new Vector3(finalScale, finalScale, finalScale);
          }
        break;

        case "Delete":
          /*if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            int layermask = LayerMask.GetMask("PlayerPlacedObject");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)) {
              Destroy(hit.collider.transform.parent.gameObject);
            }
          }*/
          Destroy(currentSelectedObject);
        break;
      }

      //TODO: Add outline to currentSelectedObject so we know what we are on
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition) {
      if (Input.touchCount > 0) {
        touchPosition = Input.GetTouch(0).position;
        return true;
      }
      touchPosition = default;
      return false;
    }

    public void ChangeCurrentSetting(string setting) { //On click button handler
      currentSetting = setting;
    }


}
