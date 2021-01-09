using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject mainOverlay, infoOverlay;
    [SerializeField] private GameObject errorText;
    [SerializeField] private GameObject mapScreen, gameScreen;

    public void ToggleMainOverlay()
    {
        if (mainOverlay.activeSelf)
        {
            errorText.SetActive(false);
            mainOverlay.SetActive(false);
        } else
        {
            mainOverlay.SetActive(true);
        }
    }

    public void ToggleInfoOverlay()
    {
        if (infoOverlay.activeSelf)
        {
            errorText.SetActive(false);
            infoOverlay.SetActive(false);
        }
        else
        {
            infoOverlay.SetActive(true);
        }
    }

    public void Play()
    {
        GameLockStatus gameStatus = GameLockStatus.Instance;
        switch (this.gameObject.name) {
            case "GhostsInTheGraveyard":
                if (gameStatus.CheckIfGameUnlocked("Ghosts In The Graveyard"))
                {
                    SceneManager.LoadScene("ShootEmUp");
                } 
                else
                {
                    errorText.SetActive(true);
                }
                break;
            case "HuntAR":
                if (gameStatus.CheckIfGameUnlocked("HuntAR"))
                {
                    SceneManager.LoadScene("BuckHunter");
                }
                else
                {
                    errorText.SetActive(true);
                }
                break;
            case "BarDiceAR":
                if (gameStatus.CheckIfGameUnlocked("AR Bar Dice"))
                {
                    SceneManager.LoadScene("BarDice");
                }
                else
                {
                    errorText.SetActive(true);
                }
                break;
            case "SamhainsRevenge":
                if (gameStatus.CheckIfGameUnlocked("Samhains Revenge"))
                {
                    SceneManager.LoadScene("SamhainsRevenge");
                }
                else
                {
                    errorText.SetActive(true);
                }
                break;
        }
    }

    public void SeeFreeLocations()
    {
        mapScreen.SetActive(true);
        switch (this.gameObject.name)
        {
            case "GhostsInTheGraveyard":
                mapScreen.GetComponent<MapController>().OnFilterChanged("hosted_games#Ghosts In The Graveyard");
                break;
            case "HuntAR":
                mapScreen.GetComponent<MapController>().OnFilterChanged("hosted_games#HuntAR");
                break;
            case "BarDiceAR":
                mapScreen.GetComponent<MapController>().OnFilterChanged("hosted_games#AR Bar Dice");
                break;
            case "SamhainsRevenge":
                mapScreen.GetComponent<MapController>().OnFilterChanged("hosted_games#Samhains Revenge");
                break;
        }
        gameScreen.SetActive(false);
    }
}
