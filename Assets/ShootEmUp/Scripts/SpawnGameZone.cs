using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//This script handles spawning the GameZone and scaling it. It handles the UI when spawning as well.
public class SpawnGameZone : MonoBehaviour
{
    public GameObject gameZone; //Make private and have get/set when incorporated into main app
    private GameObject spawnedGame;
    private bool gameHasSpawned = false;
    private Vector2 previousTouchPos;
    private float initialDistance;
    private Vector3 initialScale;

    public Text tutorialText;

    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    int counter = 0;

    void Start() {
       // tutorialText.gameObject.SetActive(true);
      spawnedGame = Instantiate(gameZone, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("AR Session Origin").transform);
    }

    void Update() {
      if(counter ==0)
        PlacePlane();
      if (Input.touchCount == 1) {
        Touch touchZero = Input.GetTouch(0);
        if (EventSystem.current.IsPointerOverGameObject(touchZero.fingerId)) {
          return;
        }
        Vector2 currentTouchPos = touchZero.position;
        spawnedGame.transform.rotation = Quaternion.Euler(0, spawnedGame.transform.rotation.eulerAngles.y + (previousTouchPos.magnitude - currentTouchPos.magnitude), 0);
        previousTouchPos = currentTouchPos;
      }
      if (Input.touchCount == 2) {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
           || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled) {
            return;
        }

        if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began) {
            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
            initialScale = spawnedGame.transform.localScale;
        } else {
            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
            if(Mathf.Approximately(initialDistance, 0)) return;
            var factor = currentDistance / initialDistance;
            spawnedGame.transform.localScale = initialScale * factor;
        }
      }
    }

    void PlacePlane()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);
        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Vector3 positionToBePlaced = hitPose.position + new Vector3(0, 0.025f, 0);
            spawnedGame.transform.position = positionToBePlaced;
        }
    }

    public void StartGameAfterScaling() { //Public onclick button handler
        Debug.Log("xx" + planeManager.isActiveAndEnabled);
      foreach (var plane in planeManager.trackables) {
        plane.gameObject.SetActive(false);
      }
      planeManager.enabled = false;
        
        counter++;
        if(counter == 2) {
          if(GameObject.FindGameObjectWithTag("ArrowFixer")!=null)
            Destroy(GameObject.FindGameObjectWithTag("ArrowFixer"));
          tutorialText.gameObject.SetActive(false);
          spawnedGame.GetComponent<InitializeGame>().InitializeUI();
          spawnedGame.GetComponent<InitializeGame>().InitializeGameScripts();
          Destroy(this);
        }
    }
}
