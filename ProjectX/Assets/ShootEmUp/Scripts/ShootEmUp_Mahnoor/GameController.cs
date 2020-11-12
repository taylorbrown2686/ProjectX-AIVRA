//mahnoor script

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public enum GameState
{
    Started,
    Paused,
    Ended
}

public class GameController : PunBehaviour
{
    public static GameController instance;
    public GameState gameState;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text otherScoreText;
    [SerializeField]
    private GameObject TimeUpImage;
    [SerializeField]
    private Text totalScoreText;

    public Button StartGameButton;
    public GameObject waitingForHost;

    [SerializeField]
    private SpawnManager spawnManager;

    public Text messageToPopUp;
    public GameObject MessagePanel;
    public Text AmmoText; //for shoot.cs

    private int score = 0;
    private int totalScore = 0;

    private int health = 4;

    
    public GameObject GameUI;
   
    public GameObject multiplayerUI;

    //to be used in HealthController.cs
    public Image vintageOverlay;   
    public Image topJaw;
    public Image bottonJaw;
    public PowerupUIController powerUpUI;
    //to be used in shootable object
    public GameObject endOfGameContainer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //StartCoroutine(PopMessage("THis is a test message"));
    }

    //start game button callback (only called by host)
    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        multiplayerUI.SetActive(false);
        GameUI.SetActive(true);
        GetComponent<RoundTimeController>().StartTimer();
        GetComponent<ShootableObjectController>().StartShootingGhost();
    }

    //will be called on all of the players in the game 
    public void ReadyGame() {

        GetComponent<RoundController>().startCountDown();
        gameState = GameState.Started;
        waitingForHost.gameObject.SetActive(false);
        spawnManager.Spawn();

    }
    
    public void AddScore() {
        score++;
        scoreText.text = "Score: " + score;
        if (PhotonNetwork.player.IsLocal) { 
            Hashtable hashtable = new Hashtable();
            hashtable["S"] = score;
            PhotonNetwork.player.SetCustomProperties(hashtable);
        }
    }

    public void decreaseHealth() {
        if (health > 0)
            health--;
        else
        {
           // endGame();
        }
    }

    //called after each round finishes
    public void RoundOver() {
        TimeUpImage.SetActive(true);
        totalScore = score;
        totalScoreText.text = "Score: "+ totalScore;
        gameState = GameState.Paused;
    }

    //called after every round
    public void RoundStart() {
        TimeUpImage.SetActive(false);
        health = 4;
        gameState = GameState.Started;
        //  score = 0;
    }

    //call this to end the game
    public void endGame() {
        gameState = GameState.Ended;    //you cannot shoot
        endOfGameContainer.SetActive(true);
    }

    private IEnumerator PopMessage(string msg) {

        messageToPopUp.text = msg;
        MessagePanel.transform.localScale = Vector3.zero;
        MessagePanel.SetActive(true);
        while (MessagePanel.transform.localScale.x <= 1)
        {
            MessagePanel.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.001f);
        }

            yield return new WaitForSeconds(5f);

            while (MessagePanel.transform.localScale.x >= 0)
            {
                MessagePanel.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                yield return new WaitForSeconds(0.001f);
            }

        MessagePanel.SetActive(false);
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
        Hashtable props = playerAndUpdatedProps[1] as Hashtable;

        if (props.ContainsKey("S")) {
            if (player != PhotonNetwork.player)
            {
                Debug.Log("Score " + (int)props["S"] + "is changed by :" + player.NickName);
                ShowPlayerScore(player.NickName, (int)props["S"]);
            }
        }
        base.OnPhotonPlayerPropertiesChanged(playerAndUpdatedProps);
    }

    void ShowPlayerScore(string name, int score) {
        otherScoreText.text = name + ": " + score;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Other player arrived: "+ newPlayer.NickName);
        
        StartCoroutine(PopMessage("New Player Arrived: " + newPlayer.NickName));
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("Other player disconnected! " + otherPlayer.ToStringFull());
    }

}
