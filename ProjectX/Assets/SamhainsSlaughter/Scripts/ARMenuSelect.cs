using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenuSelect : MonoBehaviour
{
    public string menuOption;
    public bool menuIsOn = false;
    public GameObject worldMap;
    private SamhainGun gun;

    void OnDisable() {
      gun.mapIsOpen = false;
    }

    void Update() {
      if (gun == null) {
        if (GameObject.FindGameObjectWithTag("Player")) {
          gun = GameObject.FindGameObjectWithTag("Player").GetComponent<SamhainGun>();
        }
      }
      if (Input.GetKeyDown(KeyCode.Escape)) {
        worldMap.SetActive(false);
        gun.mapIsOpen = false;
      }
    }

    void OnTriggerEnter(Collider col) {
      if (menuIsOn) {
        if (col.tag == "Bullet") {
          if (menuOption == "Arcade") {
            worldMap.SetActive(true);
            gun.mapIsOpen = true;
          } else if (menuOption == "Adventure") {
            worldMap.SetActive(true);
            gun.mapIsOpen = true;
            worldMap.GetComponent<SamhainWorldMap>().StartCoroutine(worldMap.GetComponent<SamhainWorldMap>().StartAdventure());
          }
        }
      }
    }

    public void BackFromMap() {
      worldMap.SetActive(false);
      gun.mapIsOpen = false;
    }
}
