using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDemoController : MonoBehaviour
{
    [SerializeField] private GameObject[] foodItems;
    [SerializeField] private SpawnGameZone sgz;
    [SerializeField] private GameObject uiBackground, menuScroll, sgzObj, spinToWin, backButton;

    private void OnEnable()
    {
        sgzObj.SetActive(true);
        sgz.updatePosition = true;
        spinToWin.SetActive(false);
    }

    private void OnDisable()
    {
        spinToWin.SetActive(true);
        sgzObj.SetActive(false);
    }
    public void ShowFoodItem(string item)
    {
        uiBackground.SetActive(false);
        menuScroll.SetActive(false);
        backButton.SetActive(true);
        switch (item)
        {
            case "Burger":
                sgz.gameZone = foodItems[0];
                break;
            case "Burrito":
                sgz.gameZone = foodItems[1];
                break;
            case "Pizza":
                sgz.gameZone = foodItems[2];
                break;
            case "Rice":
                sgz.gameZone = foodItems[3];
                break;
        }
        sgz.ResetGameZone();
    }

    public void BackToMenu()
    {
        sgz.updatePosition = true;
        uiBackground.SetActive(true);
        menuScroll.SetActive(true);
        backButton.SetActive(false);
    }

    public void LockObject()
    {
        sgz.updatePosition = !sgz.updatePosition;
    }
}
