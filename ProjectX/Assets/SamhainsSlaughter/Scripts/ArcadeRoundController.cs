using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeRoundController : MonoBehaviour
{
    private GameObject[] levels;
    private GameObject selectedLevel;
    public GameObject levelContainer;
    public GameObject mainMenuScene;
    public MapAreas areas;
    public GameObject pauseMenu;

    void Start() {
      levelContainer = GameObject.Find("LevelContainer");
      mainMenuScene = GameObject.Find("SamhainTerrain");
      pauseMenu = GameObject.Find("PauseMenuContainer");
      pauseMenu.SetActive(false);
      areas = this.gameObject.GetComponent<MapAreas>();
      levels = areas.areas;
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.I)) {
        Pause();
      }
    }

    public IEnumerator SetLevel(string level) {
      while (areas == null) {
        yield return new WaitForEndOfFrame();
      }
      switch (level) {
        case "Graveyard":
          selectedLevel = levels[0];
        break;
        case "Woods":
          selectedLevel = levels[1];
        break;
        case "Hills":
          selectedLevel = levels[2];
        break;
        case "Toxic":
          selectedLevel = levels[3];
        break;
        case "City":
          selectedLevel = levels[4];
        break;
        case "Castle":
          selectedLevel = levels[5];
        break;
      }
      var newArea = Instantiate(selectedLevel, levelContainer.transform.position, levelContainer.transform.rotation, levelContainer.transform);
      newArea.transform.localScale = mainMenuScene.transform.localScale / 10;
      Destroy(mainMenuScene);
    }

    public void Pause() {
      pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
