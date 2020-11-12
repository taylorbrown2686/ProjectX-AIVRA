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
    private ScoreController scoreController;
    private HealthController healthController;

    public GameObject[] countdownImages = new GameObject[3];
    public GameObject roundOverImage;
    public GameObject gameOverImage;

    public Text compositeScoreText;

    void Awake() {
      shootableController.enabled = false;
      foreach (GameObject obj in countdownImages) {
        obj.SetActive(false);
      }
      roundOverImage.SetActive(false);
   //   compositeScoreText.enabled = false;
    }

    void Start() {

      foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
        Shoot playerShoot = player.GetComponent<Shoot>();
        playerShoot.enabled = false;

        //TEMP until multiplayer implemented
        scoreController = player.GetComponent<ScoreController>();
        healthController = player.GetComponent<HealthController>();
      }
      
    }

    void Update() {

      if (roundIsActive) {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
          Shoot playerShoot = player.GetComponent<Shoot>();
          playerShoot.enabled = true;
        }
        shootableController.enabled = true;
      } else {
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
      //Round setup
      foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
        Shoot playerShoot = player.GetComponent<Shoot>();
        playerShoot.CurrentAmmo = playerShoot.MaxAmmo;
        healthController.Health = healthController.MaxHealth;
      }
      roundsRemaining -= 1;
      //Round start
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
      //Round end
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Shootable")) {
        obj.GetComponent<ShootableObject>().VanishOnRoundEnd();
      }
      roundIsActive = false;
      roundOverImage.SetActive(true);
      //compositeScoreText.enabled = true;
     // compositeScoreText.text = scoreController.CompositeScore.ToString();
      for (int i = scoreController.CompositeScore; i < scoreController.CompositeScore + scoreController.CurrentScore; i++) {
       // compositeScoreText.text = "Score: " + i.ToString();
        yield return new WaitForSeconds(0.01f);
      }
      yield return new WaitForSeconds(5f);
      roundOverImage.SetActive(false);
    //  scoreController.AddScoreToComposite(scoreController.CurrentScore);
      scoreController.CurrentScore = 0;
    //  compositeScoreText.enabled = false;
      //Play ad if on the correct round (every 3)
      if (roundsRemaining % 3 == 0) {
        Camera.main.GetComponent<BillboardedAd>().PopulateScreenWithAd();
        yield return new WaitForSeconds(10f);
        Camera.main.GetComponent<BillboardedAd>().DestroyAd();
      }
      //Check for end of game
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

    public void startCountDown() {
        StartCoroutine(CountDownRoutine());
    }
    IEnumerator CountDownRoutine() {
        //Round start
        countdownImages[0].SetActive(true); //Ready-aim-fire countdown
        yield return new WaitForSeconds(1.5f);
        countdownImages[0].SetActive(false);
        countdownImages[1].SetActive(true);
        yield return new WaitForSeconds(1.25f);
        countdownImages[1].SetActive(false);
        countdownImages[2].SetActive(true);
        roundIsActive = true;
        yield return new WaitForSeconds(1f);
        countdownImages[2].SetActive(false);
        yield return new WaitForSeconds(roundLength);
        //Round end
    }
}
