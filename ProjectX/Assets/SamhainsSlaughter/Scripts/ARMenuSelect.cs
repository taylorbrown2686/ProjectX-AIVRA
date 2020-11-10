using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenuSelect : MonoBehaviour
{
    public string menuOption;
    public bool menuIsOn = false;
    public GameObject worldMap;

    void OnTriggerEnter(Collider col) {
      if (menuIsOn) {
        if (col.tag == "Bullet") {
          if (menuOption == "Arcade") {
            worldMap.SetActive(true);
          } else if (menuOption == "Adventure") {
            worldMap.SetActive(true);
            worldMap.GetComponent<SamhainWorldMap>().StartCoroutine(worldMap.GetComponent<SamhainWorldMap>().StartAdventure());
          }
        }
      }
    }
}
