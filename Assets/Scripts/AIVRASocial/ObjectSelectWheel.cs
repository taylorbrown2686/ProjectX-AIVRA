using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//A triginometric approach to selecting an object, based on the unit circle (scale up for game)
public class ObjectSelectWheel : MonoBehaviour
{
    public GameObject[][] objectsToSpawn; //Array of spawnable objects (doesn't need getter/setter since assignment is manual)

    //These are needed because Unity DOESN'T SHOW 2D ARRAYS IN INSPECTOR FOR WHATEVER REASON. THANKS UNITY!
    [SerializeField]
    private GameObject[] greekObjects, romanObjects, victorianObjects, tropicalObjects, steampunkObjects, forestObjects, scifiObjects;

    private int categoryIndex = 0; //Index represents the category of objects selected
    public GameObject objectWheel, selectedObjectCheck; //Used to reference the wheel and collider to check for selected object
    public PlayerObjectInteractions playerInteractions;
    public GameObject wheelSettings, objectSettings; //Base of screen options to change

    void Start() {
      this.enabled = false; //Turn the wheel off
      objectsToSpawn = new GameObject[7][];
      objectsToSpawn[0] = greekObjects;
      objectsToSpawn[1] = romanObjects;
      objectsToSpawn[2] = victorianObjects;
      objectsToSpawn[3] = tropicalObjects;
      objectsToSpawn[4] = steampunkObjects;
      objectsToSpawn[5] = forestObjects;
      objectsToSpawn[6] = scifiObjects;
    }

    void OnEnable() {
      CreateWheel();
      wheelSettings.SetActive(true);
      objectSettings.SetActive(false);
    }

    void OnDisable() {
      DeleteWheel();
      wheelSettings.SetActive(false);
      objectSettings.SetActive(true);
    }

    private void CreateWheel() {
      for (int i = 0; i < objectsToSpawn[categoryIndex].Length; i++) {
        float function = (360f / objectsToSpawn[categoryIndex].Length) * i; //Function to get angle of circle object is placed on
        function = function * Mathf.Deg2Rad; //Convert to radians
        GameObject newObj = Instantiate(objectsToSpawn[categoryIndex][i], objectWheel.transform, false); //false = Don't instantiate in world space
        newObj.transform.localPosition = new Vector3(Mathf.Cos(function), 0, Mathf.Sin(function)); //Move objects to circle
        newObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f); //Set the scale smaller
        newObj.name = objectsToSpawn[categoryIndex][i].name; //Change the name since Unity appends (Clone) on the end of instantiated objects
        newObj.tag = "Placeable"; //Set the tag so they cant be interacted with like normal objects
        var rotateBehavior = newObj.AddComponent<RotateObjectContinuously>();
        rotateBehavior.XRotateSpeed = 0.5f; //Add constant rotation on each axis
        rotateBehavior.YRotateSpeed = 0.5f;
        rotateBehavior.ZRotateSpeed = 0.5f;
      }
    }

    private void DeleteWheel() {
      foreach (Transform child in objectWheel.transform) {
        Destroy(child.gameObject);
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
      if (this.enabled) {
        this.enabled = false;
      } else {
        this.enabled = true;
      }
    }

    public void ChangeObjectsInWheel(int index) { //Index represents the category of objects selected
      categoryIndex = index;
      DeleteWheel(); //Destroy the objects in the wheel
      CreateWheel(); //Create a new wheel with new index
    }

    public void SetObjectToSpawnIndex(string objName) { //Used in ObjectSelectOutline to set the active object to place
      for (int i = 0; i < objectsToSpawn[categoryIndex].Length; i++) { //For each object in the array
        if (objectsToSpawn[categoryIndex][i].name == objName) { //Compare the names
          playerInteractions.ObjectToSpawn = objectsToSpawn[categoryIndex][i]; //And set the index for the controls to be able to reference the right object
        }
      }
    }
}
