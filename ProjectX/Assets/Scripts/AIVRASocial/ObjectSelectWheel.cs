using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//A triginometric approach to selecting an object, based on the unit circle (scale up for game)
public class ObjectSelectWheel : MonoBehaviour
{
    public GameObject[] objectsToSpawn; //Array of spawnable objects (doesn't need getter/setter since assignment is manual)
    public GameObject objectWheel, selectedObjectCheck; //Used to reference the wheel and collider to check for selected object
    private int objectToSpawnIndex = 0; //Array index (changed by item select menu)
    private int totalSpawnableObjects;

    public int ObjectToSpawnIndex {get => objectToSpawnIndex; set => objectToSpawnIndex = value;} //Need this for PlayerObjectInteractions to access the index

    void Start() {
      objectWheel.SetActive(false); //Turn the wheel off
      totalSpawnableObjects = objectsToSpawn.Length; //Get the total objects in the array
      CreateWheel();
    }

    private void CreateWheel() {
      for (int i = 0; i < totalSpawnableObjects; i++) {
        float function = (360f / totalSpawnableObjects) * i; //Function to get angle of circle object is placed on
        function = function * Mathf.Deg2Rad; //Convert to radians
        GameObject newObj = Instantiate(objectsToSpawn[i], objectWheel.transform, false); //false = Don't instantiate in world space
        newObj.transform.localPosition = new Vector3(Mathf.Cos(function), 0, Mathf.Sin(function)); //Move objects to circle
        newObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f); //Set the scale smaller
        newObj.name = objectsToSpawn[i].name; //Change the name since Unity appends (Clone) on the end of instantiated objects
        newObj.tag = "Placeable"; //Set the tag so they cant be interacted with like normal objects
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
    }

    public void ToggleWheel() { //onclick listener (must be public)
      if (objectWheel.activeSelf) {
        objectWheel.SetActive(false);
      } else {
        objectWheel.SetActive(true);
      }
    }

    public void SetObjectToSpawnIndex(string objName) { //Used in ObjectSelectOutline to set the active object to place
      for (int i = 0; i < objectsToSpawn.Length; i++) { //For each object in the array
        if (objectsToSpawn[i].name == objName) { //Compare the names
          objectToSpawnIndex = i; //And set the index for the controls to be able to reference the right object
        }
      }
    }
}
