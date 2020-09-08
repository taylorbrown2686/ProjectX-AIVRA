using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A triginometric approach to selecting an object, based on the unit circle (scale up for game)
public class ObjectSelectWheel : MonoBehaviour
{
    public GameObject[] objectsToSpawn; //Array of spawnable objects (doesn't need getter/setter since assignment is manual)
    public GameObject objectWheel;
    private int objectToSpawnIndex = 0; //Array index (changed by item select menu)
    private int totalSpawnableObjects;

    public int ObjectToSpawnIndex {get; set;} //Need this for PlayerObjectInteractions to access the index

    void Start() {
      objectWheel.SetActive(false); //Turn the wheel off
      totalSpawnableObjects = objectsToSpawn.Length; //Get the total objects in the array
      CreateWheel();
    }

    private void CreateWheel() {
      for (int i = 1; i <= totalSpawnableObjects; i++) { //Start at 1 to avoid Divide-By-Zero error
        int function = (360 / totalSpawnableObjects) * i; //Function to get angle of circle object is placed on

        //Instantiate at index i - 1 since we start at 1, spawn objects along x-axis through iteration function
        GameObject newObj = Instantiate(objectsToSpawn[i - 1], objectWheel.transform, false); //false = Don't instantiate in world space
        newObj.transform.localPosition = new Vector3(Mathf.Cos(function), 0, Mathf.Sin(function)); //Move objects to circle
        newObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f); //Set the scale smaller
        newObj.name = objectsToSpawn[i - 1].name; //Change the name since Unity appends (Clone) on the end of instantiated objects
        var rotateBehavior = newObj.AddComponent<RotateObjectContinuously>();
        rotateBehavior.XRotateSpeed = 0.5f; //Add constant rotation on each axis
        rotateBehavior.YRotateSpeed = 0.5f;
        rotateBehavior.ZRotateSpeed = 0.5f;
      }
    }

    void Update() {
      if (Input.touchCount == 1) { //Rotate the object if touching screen
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved) {
          float h = touch.deltaPosition.y; //y of touchPosition
          objectWheel.transform.Rotate(0, -h, 0); //Rotate based on position of touch
        }
      }

      RaycastHit hit;
      if (Physics.Raycast(transform.position, Vector3.forward * 10, out hit, Mathf.Infinity)) {
        if (hit.transform.tag == "Placeable") {
          for (int i = 0; i < totalSpawnableObjects - 1; i++) { //Minus 1 since array starts at 0
            if (hit.transform.gameObject.name == objectsToSpawn[i].name) { //Compare names of objects together
              objectToSpawnIndex = i;
              Debug.Log(objectToSpawnIndex);
            }
          }
        }
      }
    }

    public void ToggleWheel() { //onclick listener
      if (objectWheel.activeSelf) {
        objectWheel.SetActive(false);
      } else {
        objectWheel.SetActive(true);
      }
    }
}
