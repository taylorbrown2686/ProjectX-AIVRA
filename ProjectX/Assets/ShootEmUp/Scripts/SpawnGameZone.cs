using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameZone : MonoBehaviour
{
    public GameObject gameZone; //Make private and have get/set when incorporated into main app
    private GameObject spawnedGame;
    private bool gameHasSpawned = false;

    private Vector2 initialPositionOne, initialPositionTwo; //We can't store an initial value in an Update, so we do it here
    private float initialTouchMagnitude;

    void Update() {
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (!gameHasSpawned) {
          RaycastHit hit;
          int layermask = 1 << 8;
          Ray touchPos = Camera.main.ScreenPointToRay(touch.position);
          if (Physics.Raycast(touchPos, out hit, Mathf.Infinity, layermask)) {
            spawnedGame = Instantiate(gameZone, hit.point, Quaternion.identity, GameObject.Find("AR Session Origin").transform);
            gameHasSpawned = true;
          }
        }
      }
      if (Input.touchCount == 2) {
        if (gameHasSpawned) {
          Touch touchOne = Input.GetTouch(0);
          Touch touchTwo = Input.GetTouch(1);
          Vector2 positionOne = Camera.main.ScreenToViewportPoint(touchOne.position);
          Vector2 positionTwo = Camera.main.ScreenToViewportPoint(touchTwo.position);
          if (initialPositionOne != Vector2.zero && initialPositionTwo != Vector2.zero) {
            float touchMagnitude = Vector2.Distance(positionOne, positionTwo);
            float differenceMagnitude = (initialTouchMagnitude - touchMagnitude) / 10; //Divide to make the number smaller
            spawnedGame.transform.localScale += new Vector3(differenceMagnitude, differenceMagnitude, differenceMagnitude);
          }
          initialPositionOne = positionOne;
          initialPositionTwo = positionTwo;
        }
      }
    }
}
