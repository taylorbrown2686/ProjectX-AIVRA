using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamhainWorldMap : MonoBehaviour
{
    public GameObject gameController;

    public IEnumerator StartAdventure() {
      DestroyCurrentController();
      yield return new WaitForSeconds(3f);
      gameController.AddComponent<AdventureRoundController>();
      this.gameObject.SetActive(false);
    }

    public void StartArcade(string level) {
      DestroyCurrentController();
      var roundCon = gameController.AddComponent<ArcadeRoundController>();
      roundCon.StartCoroutine(roundCon.SetLevel(level));
      this.gameObject.SetActive(false);
    }

    private void DestroyCurrentController() {
      foreach (ArcadeRoundController con in FindObjectsOfType(typeof(ArcadeRoundController))) {
        Destroy(con);
      }
      foreach (AdventureRoundController con in FindObjectsOfType(typeof(AdventureRoundController))) {
        Destroy(con);
      }
    }
}
