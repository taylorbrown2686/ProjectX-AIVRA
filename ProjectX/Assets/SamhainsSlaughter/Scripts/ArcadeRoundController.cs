using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeRoundController : MonoBehaviour
{
    private GameObject[] levels;
    private GameObject selectedLevel;
    public string selectedLevelString;
    public GameObject levelContainer;
    public GameObject player;
    public float difficultyCurve = 1;
    private bool gameHasStarted = false;
    private bool increaseDifficulty = true;
    private static ArcadeRoundController _instance;

    //Fields for Now Entering Screen
    [SerializeField] private GameObject nowEnteringScreen;

    public static ArcadeRoundController Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
      levelContainer = GameObject.Find("LevelContainer");
      player = GameObject.FindGameObjectWithTag("Player");
      levels = GlobalSamhainAssets.Instance.areas;
      var playerUI = Instantiate(GlobalSamhainAssets.Instance.playerUI, Vector3.zero, Quaternion.identity, GameObject.Find("GameUI").transform);
      playerUI.transform.SetParent(GameObject.Find("GameUI").transform, false);
      player.AddComponent<SamhainScoreController>();
      var healthCon = player.AddComponent<SamhainHealthController>();
      healthCon.smiley = GameObject.Find("Health").GetComponent<Image>();
    }

    void Update() {
      if (increaseDifficulty) {
        StartCoroutine(IncreaseDifficulty());
      }
    }

    public IEnumerator SetLevel(string level) {
      string areaName;
      int areaIndex;
      int enemyIndexOne, enemyIndexTwo;
      while (levels == null) {
        yield return new WaitForEndOfFrame();
      }
      switch (level) {
        case "Graveyard":
          selectedLevel = levels[0];
          areaName = "Ghastly Graveyard";
          areaIndex = 0;
          enemyIndexOne = 0;
          enemyIndexTwo = -1;
          StartCoroutine(NowEnteringCutscene(level, areaIndex, areaName, enemyIndexOne, enemyIndexTwo));
        break;
        case "Woods":
          selectedLevel = levels[1];
          areaName = "Crawling Copse";
          areaIndex = 1;
          enemyIndexOne = 1;
          enemyIndexTwo = -1;
          StartCoroutine(NowEnteringCutscene(level, areaIndex, areaName, enemyIndexOne, enemyIndexTwo));
        break;
        case "Hills":
          selectedLevel = levels[2];
          areaName = "Haunted Hills";
          areaIndex = 2;
          enemyIndexOne = 2;
          enemyIndexTwo = -1;
          StartCoroutine(NowEnteringCutscene(level, areaIndex, areaName, enemyIndexOne, enemyIndexTwo));
        break;
        case "Toxic":
          selectedLevel = levels[3];
          areaName = "Cherbonyl Disposal Site";
          areaIndex = 3;
          enemyIndexOne = 3;
          enemyIndexTwo = -1;
          StartCoroutine(NowEnteringCutscene(level, areaIndex, areaName, enemyIndexOne, enemyIndexTwo));
        break;
        case "City":
          selectedLevel = levels[4];
          areaName = "Necropolis";
          areaIndex = 4;
          enemyIndexOne = 4;
          enemyIndexTwo = -1;
          StartCoroutine(NowEnteringCutscene(level, areaIndex, areaName, enemyIndexOne, enemyIndexTwo));
        break;
        case "Castle":
          selectedLevel = levels[5];
          //Tell them they can't go here in arcade
        break;
        case "MainMenu":
          selectedLevel = levels[6];
          Destroy(this);
        break;
      }
    }

    private IEnumerator IncreaseDifficulty() {
      increaseDifficulty = false;
      yield return new WaitForSeconds(10f);
      difficultyCurve += 0.25f;
      increaseDifficulty = true;
    }

    private IEnumerator NowEnteringCutscene(string level, int areaIndex, string areaName, int enemyIndexOne, int enemyIndexTwo) {
      //turn on nowenteringscreen, make everything 0 opacity, fade in cooltext, then image and area name, then enemies, then enemy images 1 by 1
      var screen = Instantiate(GlobalSamhainAssets.Instance.nowEnteringUI, Vector3.zero, Quaternion.identity);
      screen.transform.SetParent(GameObject.Find("GameUI").transform, false);
      //play sound
      yield return new WaitForSeconds(0.5f);
      NowEnteringController necInstance = NowEnteringController.Instance;
      necInstance.areaImage.sprite = necInstance.areaImages[areaIndex];
      necInstance.areaText.text = areaName;
      necInstance.enemyImageOne.sprite = necInstance.enemyImages[enemyIndexOne];
      if (enemyIndexTwo != -1) {
        necInstance.enemyImageTwo.sprite = necInstance.enemyImages[enemyIndexTwo];
      }
      while (necInstance.nowEnteringScreen.color.a < 1) {
        necInstance.nowEnteringScreen.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (necInstance.nowEnteringText.color.a < 1) {
        necInstance.nowEnteringText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (necInstance.areaImage.color.a < 1) {
        necInstance.areaImage.color += new Color(0, 0, 0, 0.01f);
        necInstance.areaText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (necInstance.enemyText.color.a < 1) {
        necInstance.enemyText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (necInstance.enemyImageOne.color.a < 1) {
        necInstance.enemyImageOne.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
        if (enemyIndexTwo != -1) {
          necInstance.enemyImageTwo.color += new Color(0, 0, 0, 0.01f);
        }
      }

      var newArea = Instantiate(selectedLevel, levelContainer.transform.position, levelContainer.transform.rotation, levelContainer.transform);
      newArea.transform.localScale = levelContainer.transform.GetChild(0).transform.localScale / 10;
      selectedLevelString = level;
      Destroy(levelContainer.transform.GetChild(0).gameObject);
      Destroy(screen);
    }
}
