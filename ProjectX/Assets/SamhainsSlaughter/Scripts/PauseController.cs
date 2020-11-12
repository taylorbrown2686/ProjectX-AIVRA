using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private GameObject player;

    void Start() {
      pauseUI.SetActive(false);
    }

    void Update() {
      if (player == null) {
        player = GameObject.FindGameObjectWithTag("Player");
      }
      if (Input.GetKeyDown(KeyCode.Escape)) {
        pauseUI.SetActive(true);
        player.GetComponent<SamhainGun>().enabled = false;
        Time.timeScale = 0;
      }
    }

    public void BackToGame() {
      Time.timeScale = 1;
      player.GetComponent<SamhainGun>().enabled = true;
      pauseUI.SetActive(false);
    }

    public void RestartGame() {
      Time.timeScale = 1;
      player.GetComponent<SamhainGun>().enabled = true;
      ArcadeRoundController.Instance.SetLevel("MainMenu");
    }

    public void BackToAIVRA() {
      Time.timeScale = 1;
      SceneManager.LoadScene("AIVRAHome");
    }

}
