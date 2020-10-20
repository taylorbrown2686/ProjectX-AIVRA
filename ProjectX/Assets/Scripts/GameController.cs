using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using ExitGames.Client.Photon;

public enum GameState
{
    Started,
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

    [SerializeField]
    private SpawnManager spawnManager;
    private int score = 0;
    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
       
        GetComponent<RoundTimeController>().StartTimer();
        
    }

    public void ReadyGame() {
        gameState = GameState.Started;
        GetComponent<ShootableObjectController>().StartGame();
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

    public void RoundOver() {
        TimeUpImage.SetActive(true);
        totalScore = score;
        totalScoreText.text = "Score: "+ totalScore;
    }

    public void RoundStart() {
        TimeUpImage.SetActive(false);

      //  score = 0;
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
}
