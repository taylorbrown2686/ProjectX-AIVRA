using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//This script handles spawning the GameZone and scaling it. It handles the UI when spawning as well.
public class SpawnGameZone : MonoBehaviour
{
    public GameObject gameZone; //Make private and have get/set when incorporated into main app
    private GameObject spawnedGame;
    private bool gameHasSpawned = false;

    public Text tutorialText;
    public Slider scaleSlider, rotateSlider;

    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start() {
      tutorialText.gameObject.SetActive(true);
      spawnedGame = Instantiate(gameZone, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("AR Session Origin").transform);
    }

    void Update() {
      Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
      Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);
      if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon)) {
        Pose hitPose = hits[0].pose;
        Vector3 positionToBePlaced = hitPose.position + new Vector3(0, 0.025f, 0);
        spawnedGame.transform.position = positionToBePlaced;
      }
      spawnedGame.transform.rotation = Quaternion.Euler(0, rotateSlider.value, 0);
      spawnedGame.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
    }

    public void StartGameAfterScaling() { //Public onclick button handler
      foreach (var plane in planeManager.trackables) {
        plane.gameObject.SetActive(false);
      }
      planeManager.enabled = false;
      tutorialText.gameObject.SetActive(false);
      spawnedGame.GetComponent<InitializeGame>().InitializeUI();
      spawnedGame.GetComponent<InitializeGame>().InitializeGameScripts();
      Destroy(this);
    }
}
