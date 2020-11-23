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
    private AudioSource audioSource;
    public AudioClip evilLaugh, arcadeBlip;

    //temp
    public GameObject pizzaAd;

    IEnumerator Start() {
      GameObject.Find("LevelContainer").transform.GetChild(0).gameObject.SetActive(false);
      audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
      audioSource.clip = evilLaugh;
      audioSource.Play();
      while (background.color.a < 1) {
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
      audioSource.clip = arcadeBlip;
      int score = SamhainScoreController.Instance.score;
      int inProgressScore = 0;
      for (int i = 0; i < score / 100; i++) {
        inProgressScore += 100;
        scoreText.text = "Score: " + inProgressScore.ToString();
        audioSource.Play();
        yield return new WaitForSeconds(0.01f);
        audioSource.Stop();
      }
      for (int i = 0; i < score - ((score / 100) * 100); i++) {
        inProgressScore += 1;
        scoreText.text = "Score: " + inProgressScore.ToString();
        audioSource.Play();
        yield return new WaitForSeconds(0.01f);
        audioSource.Stop();
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
      StartCoroutine(Ad());
    }

    public void BackToAIVRA() {
      Time.timeScale = 1;
      SceneManager.LoadScene("AIVRAHome");
    }

    private IEnumerator Ad() {
      this.gameObject.SetActive(false);
      ArcadeRoundController.Instance.SetLevel("MainMenu");
      GameObject.Find("LevelContainer").transform.GetChild(0).gameObject.SetActive(false);
      var ad = Instantiate(pizzaAd, GameObject.Find("LevelContainer").transform.GetChild(0).transform.position, Quaternion.identity, GameObject.Find("LevelContainer").transform);
      yield return new WaitForSeconds(10f);
      Destroy(ad);
      GameObject.Find("LevelContainer").transform.GetChild(0).gameObject.SetActive(true);
    }
}
