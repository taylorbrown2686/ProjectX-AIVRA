using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamesController : MonoBehaviour
{

    [SerializeField] private GameObject[] gameBlocks;
    [SerializeField] private string[] gameTitles;
    private int gameCount = 0;

    private void OnEnable()
    {
        gameCount = 0;
        if (UIController.Instance.filterBusinessGames)
        {
            for (int i = 0; i < gameTitles.Length; i++)
            {
                if (GameLockStatus.Instance.gamesAndStatuses[gameTitles[i]] == true)
                {
                    gameBlocks[i].SetActive(true);
                    gameBlocks[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1050f * gameCount, 0);
                    gameCount += 1;
                }
            }
        } 
        else
        {
            foreach (GameObject obj in gameBlocks)
            {
                obj.SetActive(true);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1050f * gameCount, 0);
                gameCount += 1;
            }
        }
    }
}
