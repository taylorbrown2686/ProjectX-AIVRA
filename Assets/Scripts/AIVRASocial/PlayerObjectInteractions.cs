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
    private GameObject lastTouchedObject; //Object we have touched last
    private GameObject lastSpawnedObject; //Object we spawned last (NOT THE SAME AS lastTouchedObject)
    private GameObject objectToSpawn; //Object selected in ObjectSelectWheel
    private GameObject activeAxes; //Represents the axes spawned for the current setting
    public GameObject[] axes = new GameObject[3]; //Represents all the axes we can spawn for move, rotate, and scale
    public Transform arso; //ARSessionOrigin, SelectedObject (to spawn with create button)
    public ObjectSelectWheel osw; //Wheel containing all the selectable models
    public Shader outlineShader, defaultShader;

    public ARRaycastManager raycastManager; //Raycast manager to store hit objects
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject ObjectToSpawn {set => objectToSpawn = value;}

    private void Update() {
      if (!TryGetTouchPosition(out Vector2 touchPosition) || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
        return; //Break without executing anything if we aren't touching an object
      }

      //Select object raycast
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began && currentSetting != "Spawn") { //If on 'Spawn', single touch spawns object, not select
          Ray ray = Camera.main.ScreenPointToRay(touchPosition); //Get touch position
          RaycastHit hit;
          int layermask = (1 << 8 | 1 << 10);
          if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask)) {
            if (hit.collider.gameObject.layer == 8) {
              if (lastTouchedObject != null) { //Null check so the first spawned object doesn't throw an error
                lastTouchedObject.GetComponent<PlaceableObject>().GhostObject(false);
                lastTouchedObject = null;
                Destroy(activeAxes); //Destroy the current axis so we can spawn a new one
              }
              lastTouchedObject = hit.collider.gameObject; //Set the active object via a raycast
              lastTouchedObject.GetComponent<PlaceableObject>().GhostObject(true);
            } else if (hit.collider.gameObject.layer == 10) {
              return; //If we hit an axis, the AxesBase will handle the raycast
            }
          }
        }
      }

      //Switch-case for current setting
      if (!osw.enabled) { //Make sure we aren't in the wheel
        switch (currentSetting) { //Switch between settings to change what we look for in touches.
          case "Spawn":
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
              if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
                var hitPose = hits[0].pose;
                lastSpawnedObject = Instantiate(objectToSpawn,
                  hitPose.position + new Vector3(0, 0.5f, 0), hitPose.rotation, arso); //Spawn at finger touch
                lastSpawnedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //Make the object smaller on spawn.
                lastSpawnedObject.layer = 8; //Set the layer to PlayerPlacedObjects so we can interact with it.
                lastSpawnedObject.GetComponent<PlaceableObject>().enabled = true;
                lastSpawnedObject.GetComponent<PlaceableObject>().GhostObject(true); //Ghost the object so it doesn't collide until spawned
              }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) {
              if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes)) {
                var hitPose = hits[0].pose;
                lastSpawnedObject.transform.position = hitPose.position + new Vector3(0, 0.5f, 0);
              }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
              if (lastSpawnedObject) { //If the lastSpawnedObject exists
                lastSpawnedObject.GetComponent<PlaceableObject>().GhostObject(false);
              }
            }
          break;

          case "Move":
            if (lastTouchedObject && !activeAxes) { //If last touched exists (not null)
              activeAxes = Instantiate(axes[0], lastTouchedObject.transform.position + new Vector3(0, 0.1f, 0),
                Quaternion.identity, lastTouchedObject.transform);
            }
          break;

          case "Rotate":
            if (lastTouchedObject && !activeAxes) { //If last touched exists (not null)
              activeAxes = Instantiate(axes[1], lastTouchedObject.transform.position + new Vector3(0, 0.1f, 0),
                Quaternion.identity, lastTouchedObject.transform);
            }
          break;

          case "Scale":
            if (lastTouchedObject && !activeAxes) { //If last touched exists (not null)
              activeAxes = Instantiate(axes[2], lastTouchedObject.transform.position + new Vector3(0, 0.1f, 0),
                Quaternion.identity, lastTouchedObject.transform);
            }
          break;

          case "Delete":
            Destroy(lastTouchedObject);
          break;
        }
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
      if (lastTouchedObject) { //If a lastTouchedObject exists
        if (lastTouchedObject.GetComponent<PlaceableObject>().validAreaToPlace) {
          currentSetting = setting; //Change the setting
          if (activeAxes) { //Delete the current axis if there is one
            Destroy(activeAxes);
          }
          lastTouchedObject.GetComponent<PlaceableObject>().GhostObject(false);
          lastTouchedObject = null;
        }
      } else {
        currentSetting = setting; //Change the setting
      }
    }


}
