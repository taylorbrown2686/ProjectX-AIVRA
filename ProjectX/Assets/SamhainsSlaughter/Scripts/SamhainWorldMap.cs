using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamhainWorldMap : MonoBehaviour
{
    public GameObject gameController;

    public IEnumerator StartAdventure() {
      yield return new WaitForSeconds(3f);
      gameController.AddComponent<AdventureRoundController>();
      this.gameObject.SetActive(false);
    }

    public void StartArcade(string level) {
      var roundCon = gameController.AddComponent<ArcadeRoundController>();
      roundCon.StartCoroutine(roundCon.SetLevel(level));
      this.gameObject.SetActive(false);
    }
}
