using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidBackFromGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private bool paused = false;

    void Start() {
      pauseScreen.SetActive(false);
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        if (!paused) {
          Pause();
        } else {
          BackOutOfPause();
        }
      }
    }

    private void Pause() {
      paused = true;
      pauseScreen.SetActive(true);
      Time.timeScale = 0;
    }

    public void BackOutOfPause() {
      paused = false;
      Time.timeScale = 1;
      pauseScreen.SetActive(false);
    }

    public void BackToHome() {
      paused = false;
      Time.timeScale = 1;
      SceneManager.LoadScene("AIVRAHome");
    }

    public void RestartGame(string nameOfGame) {
      paused = false;
      Time.timeScale = 1;
      SceneManager.LoadScene(nameOfGame);
    }
}
