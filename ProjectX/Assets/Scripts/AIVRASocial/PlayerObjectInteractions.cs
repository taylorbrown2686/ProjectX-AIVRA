using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//This script is disgusting in terms of the approach taken to implement this feature.
//This is the only way to do it though, considering ARRaycasts cannot return a GameObject's name
//You can thank Unity for this mess of a script
public class PlayerObjectInteractions : MonoBehaviour
{
    private string currentSetting; //Current setting we are on
    private GameObject currentSelectedObject; //Object we have touched last
    public Transform arso; //ARSessionOrigin
    public ObjectSelectWheel osw; //Wheel containing all the selectable models

    public ARRaycastManager raycastManager; //Raycast manager to store hit objects
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Text debugText; //TODO: REMOVE THIS ONCE TESTED

    private void Update() {
      if (!TryGetTouchPosition(out Vector2 touchPosition) || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { //Break without executing anything if we aren't touching an object
        return;
      }

      if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) { //If beginning of touch...
        Ray ray = Camera.main.ScreenPointToRay(touchPosition); //Get touch position
        RaycastHit hit;
        int layermask = LayerMask.GetMask("PlayerPlacedObject");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)) {
          currentSelectedObject = hit.collider.gameObject; //Set the active object via a raycast
        }
      }

      switch (currentSetting) { //Switch between settings to change what we look for in touches.
        case "Spawn":
          if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
              var hitPose = hits[0].pose;
              var newObj = Instantiate(osw.objectsToSpawn[osw.ObjectToSpawnIndex], hitPose.position, hitPose.rotation, arso);
              newObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //Make the object smaller on spawn.
              newObj.layer = 8; //Set the layer to PlayerPlacedObjects so we can interact with it.
            }
          }
        break;

        case "Move":
          if (Input.touchCount == 1) {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
              var hitPose = hits[0].pose;
              currentSelectedObject.transform.position = hitPose.position; //Update the position
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
            debugText.text = finalScale.ToString();
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
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition) { //Checks if we are touching something
      if (Input.touchCount > 0) {
        touchPosition = Input.GetTouch(0).position;
        return true;
      }
      touchPosition = default;
      return false;
    }

    public void ChangeCurrentSetting(string setting) { //On click button handler
      currentSetting = setting;
      currentSelectedObject = null; //Fixes a bug where changing to 'delete' deletes the selected object
    }


}
