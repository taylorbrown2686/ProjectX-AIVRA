using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class DiceCommunication : MonoBehaviourPunCallbacks
{

    public ShakeDice shakeDice;
    int counter;
    GameObject scoreBoard;
    // Start is called before the first frame update
    int maxScore, activePlayers;
    string maxNickname;
    public void StartGame()
    {
        
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true) {

            if(PhotonNetwork.PlayerList.Length>2)
                photonView.RPC("SetMaxTurn", RpcTarget.All, 1);
            else
                photonView.RPC("SetMaxTurn", RpcTarget.All, 3);

            SendTurnPlayed();
        }
    }


    private void Start()
    {
        scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
    }

    public void TurnPlayed()
    {
        DiceGameManager.Instance.dsm.Refresh();
        shakeDice.playersTurn = false;
        shakeDice.turnCounter = 0;
        photonView.RPC("SendTurnPlayed", RpcTarget.MasterClient);
        
    }

    [PunRPC]
    void SendTurnPlayed()
    {
        if (counter == (PhotonNetwork.PlayerList.Length)) {
            print("Round ends");


            maxScore = int.MinValue;
            maxNickname = "";
            activePlayers = 0;
            DicePlayer dp;

            foreach (Transform child in scoreBoard.transform)
            {
                dp = child.GetComponent<DicePlayer>();
                if (dp.isOut == true)
                    continue;
                if(dp.score > maxScore)
                {
                    maxScore = dp.score;
                    maxNickname = dp.nickname;
                }
            }


            bool doubleWinner = false;
            foreach (Transform child in scoreBoard.transform)
            {
                dp = child.GetComponent<DicePlayer>();
                if (dp.score == maxScore && maxNickname != dp.nickname)
                {
                    doubleWinner = true;
                    break;
                }
                
            }

            if (doubleWinner == false)
            {
                photonView.RPC("SetOut", RpcTarget.All, maxNickname);
                print("out!!!!!!!!!!!");
            }

            activePlayers = 0;
            foreach (Transform child in scoreBoard.transform)
            {
                dp = child.GetComponent<DicePlayer>();

                if (dp.isOut == false)
                {
                    activePlayers++;
                }
                
            }

            if (activePlayers <= 1) { 
                photonView.RPC("EndGame", RpcTarget.All);
                return;
            }

            if (activePlayers > 2)
                photonView.RPC("SetMaxTurn", RpcTarget.All, 1);
            else
                photonView.RPC("SetMaxTurn", RpcTarget.All, 3);

            print(maxNickname);

            counter = 0;
        }

        print(counter);

        if(scoreBoard.GetComponentsInChildren<DicePlayer>()[counter].isOut == false) { 

            photonView.RPC("SendTurnInfo", RpcTarget.All, PhotonNetwork.PlayerList[counter].NickName);
            counter++;
            
        }
        else {
            counter++;
            SendTurnPlayed();
        }
    }

    public void MasterSendScaleMessage()
    {
        GameObject gameZone = GameObject.FindGameObjectWithTag("GameZone");
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
            photonView.RPC("SetScale", RpcTarget.Others, gameZone.transform.localScale.x, gameZone.transform.localScale.z);
    }

    [PunRPC]
    void SetScale(float x, float z)
    {
        Debug.Log("fixed scale!!!!!!!!!!!");
        GameObject gameZone = GameObject.FindGameObjectWithTag("GameZone");
        FixedScale fs = gameZone.GetComponent<FixedScale>();
        fs.enabled = true;
        fs.SetScale(x, z);
    }



    [PunRPC]
    void SendTurnInfo(string nickname)
    {
        shakeDice.UpCups();
        foreach (Transform child in scoreBoard.transform)
        {
            if (child.GetComponentInChildren<Text>().text == nickname)
                child.GetComponentsInChildren<Image>()[1].enabled = true;
            else
                child.GetComponentsInChildren<Image>()[1].enabled = false;
        }
        if (nickname == PhotonNetwork.LocalPlayer.NickName) { 
            shakeDice.playersTurn = true;
            DiceGameManager.Instance.myTurn = true;
            DiceGameManager.Instance.nd.gameObject.SetActive(false);
            shakeDice.turnCounter = 0;
        }
        else { 
            shakeDice.playersTurn = false;
            DiceGameManager.Instance.myTurn = false;
            DiceGameManager.Instance.nd.gameObject.SetActive(true);
        }
    }

    public void CallSendDices(int[] value, bool[] selected)
    {
        photonView.RPC("SendDices", RpcTarget.Others, (object)value, (object)selected);
    }

    [PunRPC]
    void SendDices(int[] value, bool[] selected)
    {
        DiceGameManager.Instance.nd.UpdateDices(value, selected);
    }

    public void SendMyScore(string scoreInText, int score)
    {
        photonView.RPC("UpdateScore", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, scoreInText, score);
    }

    public void CallEndTurn()
    {
        int temp = shakeDice.turnCounter;
        shakeDice.RemotePickup();
        shakeDice.playersTurn = false;
        print(temp);
        photonView.RPC("SetMaxTurn", RpcTarget.All, (int)(temp + 1));
        TurnPlayed();
        DiceGameManager.Instance.endTurn.gameObject.SetActive(false);
        
    }

    [PunRPC]
    public void SetMaxTurn(int maxTurns)
    {
        shakeDice.maxTurns = maxTurns;
    }


    [PunRPC]
    void UpdateScore(string nickname, string scoreInText, int score)
    {
        DicePlayer dp;
        foreach (Transform child in scoreBoard.transform)
        {
            dp = child.GetComponent<DicePlayer>();
            if (dp.nicknameText.text == nickname) { 
                dp.scoreText.text = scoreInText;
                dp.score = score;
            }
        }


    }

    [PunRPC]
    void SetOut(string nickname)
    {
        print("stream out!!!!!!!!!!!");
        print(nickname);
        DicePlayer dp;
        foreach (Transform child in scoreBoard.transform)
        {
            dp = child.GetComponent<DicePlayer>();
            if (dp.nicknameText.text == nickname)
            {
                dp.scoreText.text = "Out!";
                dp.scoreText.color = Color.green;
                dp.isOut = true;
                dp.score = -1;
            }
        }


    }

    [PunRPC]
    void EndGame()
    {
        DicePlayer dp;
        foreach (Transform child in scoreBoard.transform)
        {
            dp = child.GetComponent<DicePlayer>();
            dp.turnImage.SetActive(false);
            if (dp.isOut == false)
            {
                dp.scoreText.text = "Loser!";
                dp.scoreText.color = Color.red;
            }
        }

    }
}
