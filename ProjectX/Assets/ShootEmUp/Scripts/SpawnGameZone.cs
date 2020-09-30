using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
//This script handles spawning the GameZone and scaling it. It handles the UI when spawning as well.
public class SpawnGameZone : MonoBehaviour
{
    public GameObject gameZone; //Make private and have get/set when incorporated into main app
    private GameObject spawnedGame;
    private bool gameHasSpawned = false;
    private Vector2 previousTouchPosition;

    public Text placeZoneText, scaleZoneText;

    public ARPlaneManager planeManager;

    void Start() {
      placeZoneText.gameObject.SetActive(true);
      scaleZoneText.gameObject.SetActive(false);
    }

    void Update() {
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (!gameHasSpawned) {
          RaycastHit hit;
          int layermask = 1 << 8;
          Ray touchPos = Camera.main.ScreenPointToRay(touch.position);
          if (Physics.Raycast(touchPos, out hit, Mathf.Infinity, layermask)) {
            spawnedGame = Instantiate(gameZone, hit.point + new Vector3(0, 0.05f, 0),
              Quaternion.identity, GameObject.Find("AR Session Origin").transform);
            gameHasSpawned = true;
            planeManager.enabled = false;
          }
        }
        if (gameHasSpawned) {
          placeZoneText.gameObject.SetActive(false);
          scaleZoneText.gameObject.SetActive(true);
          Vector2 touchPosition = Camera.main.ScreenToViewportPoint(touch.position);
          if (previousTouchPosition.magnitude != 0) {
            if (previousTouchPosition.y - touchPosition.y > 0) {
              spawnedGame.transform.localScale += new Vector3(-0.001f, -0.001f, -0.001f);
            } else if (previousTouchPosition.y - touchPosition.y <= 0) {
              spawnedGame.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            }
          }
          previousTouchPosition = touchPosition;
        }
      }
    }

    public void StartGameAfterScaling() { //Public onclick button handler
      scaleZoneText.gameObject.SetActive(false);
      spawnedGame.GetComponent<InitializeGame>().InitializeGameScripts();
      spawnedGame.GetComponent<InitializeGame>().InitializeUI();
      Destroy(this);
    }
}
