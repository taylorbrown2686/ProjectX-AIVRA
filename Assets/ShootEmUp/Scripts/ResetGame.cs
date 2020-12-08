using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameObject gameZonePrefab;
    [SerializeField] private GameObject playerPrefab;
    private GameObject currentGameZone;
    private GameObject[] currentPlayers;
    private int playersToRespawn;

    void Start() {
      currentGameZone = GameObject.Find("GameZone(Clone)");
      currentPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    /*public void ReplayGame() {
      Vector3 pos = currentGameZone.transform.position;
      Quaternion rot = currentGameZone.transform.rotation;
      Vector3 scale = currentGameZone.transform.localScale;
      foreach (GameObject obj in currentPlayers) {
        Destroy(obj);
        playersToRespawn += 1;
      }
      Destroy(currentGameZone);
      for (int i = 0; i < playersToRespawn; i++) {
        Instantiate(playerPrefab, pos, rot, Camera.main.transform);
      }
      GameObject newGame = Instantiate(gameZonePrefab, pos, rot, GameObject.Find("AR Session Origin").transform);
      newGame.transform.localScale = scale;
    }*/

    public void ReplayGame() {
      SceneManager.LoadScene("ShootEmUp");
    }
}
