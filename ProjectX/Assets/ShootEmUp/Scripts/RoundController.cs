using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    private int roundsRemaining;
    private int roundLength;
    private bool gameReadyToStart = false; //Is the game ready to begin the first round?
    public int RoundsRemaining {get => roundsRemaining; set => roundsRemaining = value;}
    public int RoundLength {get => roundLength; set => roundLength = value;}
    public bool GameReadyToStart {get => gameReadyToStart; set => gameReadyToStart = value;}
    private bool roundIsActive = false;

    public ShootableObjectController shootableController; //Target spawn control script
    public ScoreController scoreController;

    public GameObject[] countdownImages = new GameObject[3];
    public GameObject roundOverImage;
    public GameObject gameOverImage;

    public Text compositeScoreText;

    void Start() {
      //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
      //  Shoot playerShoot = player.GetComponent<Shoot>();
      //  playerShoot.enabled = false;

      //  scoreController = player.GetComponent<ScoreController>(); //TEMP

      //}
      //shootableController.enabled = false;
      //foreach (GameObject obj in countdownImages) {
      //  obj.SetActive(false);
      //}
      //roundOverImage.SetActive(false);
      //gameOverImage.SetActive(false);
      //compositeScoreText.enabled = false;
    }

    void Update() {
      if (roundIsActive) {
        //shoot.enabled = true;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
          Shoot playerShoot = player.GetComponent<Shoot>();
          playerShoot.enabled = true;
        }
        shootableController.enabled = true;
      } else {
        //shoot.enabled = false;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
          Shoot playerShoot = player.GetComponent<Shoot>();
          playerShoot.enabled = false;
        }
        shootableController.enabled = false;
      }
      if (gameReadyToStart) {
        StartCoroutine(StartRound());
        gameReadyToStart = false;
      }
    }

    public IEnumerator StartRound() {
      foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
        Shoot playerShoot = player.GetComponent<Shoot>();
        playerShoot.CurrentAmmo = playerShoot.MaxAmmo;
      }
      roundsRemaining -= 1;

      countdownImages[0].SetActive(true); //Ready-aim-fire countdown
      yield return new WaitForSeconds(3f);
      countdownImages[0].SetActive(false);
      countdownImages[1].SetActive(true);
      yield return new WaitForSeconds(1.5f);
      countdownImages[1].SetActive(false);
      countdownImages[2].SetActive(true);
      roundIsActive = true;
      yield return new WaitForSeconds(1f);
      countdownImages[2].SetActive(false);

      yield return new WaitForSeconds(roundLength);
      roundIsActive = false;
      roundOverImage.SetActive(true);
      compositeScoreText.enabled = true;
//      compositeScoreText.text = scoreController.CompositeScore.ToString();
      //for (int i = 0; i < scoreController.CurrentScore; i++) {
      //  compositeScoreText.text = "Score: " + i.ToString();
      //  yield return new WaitForSeconds(0.01f);
      //}
      yield return new WaitForSeconds(5f);
      roundOverImage.SetActive(false);
      compositeScoreText.enabled = false;

      if (roundsRemaining != 0) {
        StartCoroutine(StartRound());
      } else {
        gameOverImage.SetActive(true);
        //for (int i = 0; i < scoreController.CompositeScore; i++) {
        //  compositeScoreText.text = "Score: " + i.ToString();
        //  yield return new WaitForSeconds(0.01f);
        //}
      }
    }
}
