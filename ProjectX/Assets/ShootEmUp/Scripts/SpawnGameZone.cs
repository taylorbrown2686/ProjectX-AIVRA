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

    [SerializeField]
    private GameObject tutorialCanvas;
    //mahnoor
    [SerializeField]
    private InputField nameInputField;
    [SerializeField]
    private GameObject namePanel;
    //end mahnoor
    public Slider scaleSlider, rotateSlider;

    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start() {
        if (!PlayerPrefs.HasKey("Name"))
        {
            namePanel.SetActive(true);

            // tutorialCanvas.SetActive(false);
        }
        else
        {
            tutorialCanvas.SetActive(true);
            namePanel.SetActive(false);
        }

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

        tutorialCanvas.SetActive(false);
    //  spawnedGame.GetComponent<InitializeGame>().InitializeGameScripts();
       spawnedGame.GetComponent<InitializeGame>().SwitchMultiplayerUI(true);

      Destroy(this);

    }

    //mahnoor
    public void BeginCallback() {

        string name = nameInputField.text;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name)) {
            PlayerPrefs.SetString("Name", name);
            namePanel.SetActive(false);
            tutorialCanvas.SetActive(true);
        }

    }
    //end mahnoor
}
