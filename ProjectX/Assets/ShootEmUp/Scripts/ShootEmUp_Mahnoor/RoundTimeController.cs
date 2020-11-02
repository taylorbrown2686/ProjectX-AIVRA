using System;
using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

using ExitGames.Client.Photon;

#pragma warning disable 649 

// the Photon server assigns a ActorNumber (player.ID) to each player, beginning at 1
// for this game, we don't mind the actual number
// this game uses player 0 and 1, so clients need to figure out their number somehow
public class RoundTimeController : PunBehaviour, IPunTurnManagerCallbacks
{

    //[SerializeField]
    //private Text TurnText;

    [SerializeField]
    private Text TimeText;

    //[SerializeField]
    //private Text RemotePlayerText;

    //[SerializeField]
    //private Text LocalPlayerText;
    
  
  


    //[SerializeField]
    //private RectTransform DisconnectedPanel;

    //private ResultType result;

    private PunTurnManager turnManager;

  //  public Hand randomHand;    // used to show remote player's "hand" while local player didn't select anything

	// keep track of when we show the results to handle game logic.
	//private bool IsShowingResults;
	
    //public enum Hand
    //{
    //    None = 0,
    //    Rock,
    //    Paper,
    //    Scissors
    //}

    //public enum ResultType
    //{
    //    None = 0,
    //    Draw,
    //    LocalWin,
    //    LocalLoss
    //}

    public void Start()
    {
		this.turnManager = this.gameObject.AddComponent<PunTurnManager>();
        this.turnManager.TurnManagerListener = this;
        this.turnManager.TurnDuration = 30f;
        
       
        //this.localSelectionImage.gameObject.SetActive(false);
        //this.remoteSelectionImage.gameObject.SetActive(false);
      //  this.StartCoroutine("CycleRemoteHandCoroutine");

		RefreshUIViews();
    }

    public void Update()
    {
		// Check if we are out of context, which means we likely got back to the demo hub.
		//if (this.DisconnectedPanel ==null)
		//{
		//	Destroy(this.gameObject);
		//}

        // for debugging, it's useful to have a few actions tied to keys:
        if (Input.GetKeyUp(KeyCode.L))
        {
            PhotonNetwork.LeaveRoom();
        }
        //if (Input.GetKeyUp(KeyCode.C))
        //{
        //    PhotonNetwork.ConnectUsingSettings(null);
        //    PhotonHandler.StopFallbackSendAckThread();
        //}

	
        if ( ! PhotonNetwork.inRoom)
        {
			return;
		}

		// disable the "reconnect panel" if PUN is connected or connecting
		//if (PhotonNetwork.connected && this.DisconnectedPanel.gameObject.GetActive())
		//{
		//	this.DisconnectedPanel.gameObject.SetActive(false);
		//}
		//if (!PhotonNetwork.connected && !PhotonNetwork.connecting && !this.DisconnectedPanel.gameObject.GetActive())
		//{
		//	this.DisconnectedPanel.gameObject.SetActive(true);
		//}


		if (PhotonNetwork.room.PlayerCount>=1)
		{
			if (this.turnManager.IsOver)
			{
				return;
			}

			/*
			// check if we ran out of time, in which case we loose
			if (turnEnd<0f && !IsShowingResults)
			{
					Debug.Log("Calling OnTurnCompleted with turnEnd ="+turnEnd);
					OnTurnCompleted(-1);
					return;
			}
		*/

            //if (this.TurnText != null)
            //{
            //    this.TurnText.text = this.turnManager.Turn.ToString();
            //}

			if (this.turnManager.Turn > 0 && this.TimeText != null )
            {
//                Debug.Log("Timer is running");
				TimeText.text = this.turnManager.RemainingSecondsInTurn.ToString("F1") + " SECONDS";

				//TimerFillImage.anchorMax = new Vector2(1f- this.turnManager.RemainingSecondsInTurn/this.turnManager.TurnDuration,1f);
            }

            
		}

		

        // show local player's selected hand
       

        // remote player's selection is only shown, when the turn is complete (finished by both)
        if (this.turnManager.IsCompletedByAll)
        {
            //selected = SelectionToSprite(this.remoteSelection);
            //if (selected != null)
            //{
            //    this.remoteSelectionImage.color = new Color(1,1,1,1);
            //    this.remoteSelectionImage.sprite = selected;
            //}
        }
        else
        {
			//ButtonCanvasGroup.interactable = PhotonNetwork.room.PlayerCount > 1;

   //         if (PhotonNetwork.room.PlayerCount < 2)
   //         {
   //             this.remoteSelectionImage.color = new Color(1, 1, 1, 0);
   //         }

            // if the turn is not completed by all, we use a random image for the remote hand
            if (this.turnManager.Turn > 0 && !this.turnManager.IsCompletedByAll)
            {
                
            }
        }

    }

    #region TurnManager Callbacks

    /// <summary>Called when a turn begins (Master Client set a new Turn number).</summary>
    public void OnTurnBegins(int turn)
    {
        Debug.Log("OnTurnBegins() turn: "+ turn);

        if (turn == 1)
        {
            GameController.instance.ReadyGame();
        }
        //ButtonCanvasGroup.interactable = true;
    }


    public void OnTurnCompleted(int obj)
    {
       
        Debug.Log("OnTurnCompleted: " + obj);
       
        GameController.instance.RoundOver();
      // GameController.instance.
        this.OnEndTurn();
    }


    // when a player moved (but did not finish the turn)
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)
    {
        Debug.Log("OnPlayerMove: " + photonPlayer + " turn: " + turn + " action: " + move);
        throw new NotImplementedException();
    }


    // when a player made the last/final move in a turn
    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)
    {
        Debug.Log("OnTurnFinished: " + photonPlayer + " turn: " + turn + " action: " + move);

        
    }



    public void OnTurnTimeEnds(int obj)
    {
		//if (!IsShowingResults)
		//{
		//	Debug.Log("OnTurnTimeEnds: Calling OnTurnCompleted");
			OnTurnCompleted(-1);
		//}
	}


    #endregion

    #region Core Gameplay Methods

    
    /// <summary>Call to start the turn (only the Master Client will send this).</summary>
    public void StartTurn()
    {
        if (PhotonNetwork.isMasterClient)
        {
            
            this.turnManager.BeginTurn();
        }
    }
	
  
	
    public void OnEndTurn()
    {
      StartCoroutine("ShowResultsBeginNextTurnCoroutine");
    }

    public IEnumerator ShowResultsBeginNextTurnCoroutine()
    {

        //	IsShowingResults = true;
        int turn = turnManager.Turn;
        yield return new WaitForSeconds(2.0f);
        Debug.Log("New Turn Starts");
       
        if (turn <= 10)
        {
            GameController.instance.RoundStart();
        }
        else
        {
            GameController.instance.endGame();
        }
        this.StartTurn();
    }


    public void EndGame()
    {
		Debug.Log("EndGame");
    }


    public void StartTimer() {
        if (turnManager.Turn == 0 )
        {
            StartTurn();
            
        }
    }

    private void UpdatePlayerTexts()
    {
        PhotonPlayer remote = PhotonNetwork.player.GetNext();
        PhotonPlayer local = PhotonNetwork.player;

        if (remote != null)
        {
            // should be this format: "name        00"
          //  this.RemotePlayerText.text = remote.NickName + "        " + remote.GetScore().ToString("D2");
        }
        else
        {

			
			this.TimeText.text = "";
           // this.RemotePlayerText.text = "waiting for another player        00";
        }
        
        if (local != null)
        {
            // should be this format: "YOU   00"
           // this.LocalPlayerText.text = "YOU   " + local.GetScore().ToString("D2");
        }
    }


    #endregion


    #region Handling Of Buttons

   
    public void OnClickConnect()
    {
        PhotonNetwork.ConnectUsingSettings(null);
      //  PhotonHandler.StopFallbackSendAckThread();  // this is used in the demo to timeout in background!
    }
    
    public void OnClickReConnectAndRejoin()
    {
        PhotonNetwork.ReconnectAndRejoin();
      //  PhotonHandler.StopFallbackSendAckThread();  // this is used in the demo to timeout in background!
    }

    #endregion

	void RefreshUIViews()
	{
		//TimerFillImage.anchorMax = new Vector2(0f,1f);

		//ConnectUiView.gameObject.SetActive(!PhotonNetwork.inRoom);
		//GameUiView.gameObject.SetActive(PhotonNetwork.inRoom);

		//ButtonCanvasGroup.interactable = PhotonNetwork.room!=null?PhotonNetwork.room.PlayerCount > 1:false;
	}


    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom()");



		RefreshUIViews();
    }

  

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
		Debug.Log("Other player arrived");

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            if (this.turnManager.Turn == 0)
            {
                // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
               // this.StartTurn();
            }
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
		Debug.Log("Other player disconnected! "+otherPlayer.ToStringFull());
    }


    public override void OnConnectionFail(DisconnectCause cause)
    {
      //  this.DisconnectedPanel.gameObject.SetActive(true);
    }

}
