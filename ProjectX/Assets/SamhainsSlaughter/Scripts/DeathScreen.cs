using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Image background, samhain, madeYouText, insaneText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject mainMenuButton, aivraButton;

    IEnumerator Start() {
      while (background.color.a < 1) {
        //evil laugh sfx
        background.color += new Color(0, 0, 0, 0.01f);
        samhain.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (madeYouText.color.a < 1) {
        madeYouText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (insaneText.color.a < 1) {
        insaneText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      while (scoreText.color.a < 1) {
        scoreText.color += new Color(0, 0, 0, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      for (int i = 0; i < SamhainScoreController.Instance.score; i++) {
        scoreText.text = "Score: " + i;
        yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(1f);
      mainMenuButton.SetActive(true);
      yield return new WaitForSeconds(1f);
      aivraButton.SetActive(true);
    }

    public void Restart() {
      Time.timeScale = 1;
      ArcadeRoundController.Instance.SetLevel(ArcadeRoundController.Instance.selectedLevelString);
    }

    public void BackToMainMenu() {
      Time.timeScale = 1;
      ArcadeRoundController.Instance.SetLevel("MainMenu");
    }

    public void BackToAIVRA() {
      Time.timeScale = 1;
      SceneManager.LoadScene("AIVRAHome");
    }
}
