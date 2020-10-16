using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer, gamesAndNavContainer;

    public void ChangePage(string pageToOpen) {
      DisableAllPages();
      switch (pageToOpen) {
        case "MainMenu":
          mainMenuContainer.SetActive(true);
        break;

        case "Games":
          gamesAndNavContainer.SetActive(true);
        break;

        case "Entertainment":
          gamesAndNavContainer.SetActive(true);
        break;

        case "Navigation":
          gamesAndNavContainer.SetActive(true);
        break;
      }
    }

    private void DisableAllPages() {
      mainMenuContainer.SetActive(false);
      gamesAndNavContainer.SetActive(false);
    }
}
