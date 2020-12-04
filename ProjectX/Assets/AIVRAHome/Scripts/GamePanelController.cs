using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject mainOverlay, infoOverlay;

    public void ToggleMainOverlay()
    {
        if (mainOverlay.activeSelf)
        {
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
                if (gameStatus.CheckIfGameUnlocked("Ghosts in the Graveyard"))
                {
                    SceneManager.LoadScene("ShootEmUp");
                } 
                else
                {
                    //error text
                }
                break;
            case "HuntAR":
                if (gameStatus.CheckIfGameUnlocked("HuntAR"))
                {
                    SceneManager.LoadScene("BuckHunter");
                }
                else
                {
                    //error text
                }
                break;
            case "BarDiceAR":
                if (gameStatus.CheckIfGameUnlocked("AR Bar Dice"))
                {
                    SceneManager.LoadScene("BarDice");
                }
                else
                {
                    //error text
                }
                break;
            case "SamhainsRevenge":
                if (gameStatus.CheckIfGameUnlocked("Samhains Revenge"))
                {
                    //SceneManager.LoadScene("SamhainsRevenge");
                }
                else
                {
                    //error text
                }
                break;
        }
    }
}
