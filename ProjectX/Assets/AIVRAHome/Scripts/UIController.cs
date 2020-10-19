using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer, mapContainer, gamesContainer;

    void Start() {
      DisableAllPages();
      mainMenuContainer.SetActive(true);
    }

    public void ChangePage(string pageToOpen) {
      DisableAllPages();
      switch (pageToOpen) {
        case "MainMenu":
          mainMenuContainer.SetActive(true);
        break;

        case "Map":
          mapContainer.SetActive(true);
        break;

        case "Games":
          gamesContainer.SetActive(true);
        break;

        case "Entertainment":
          gamesContainer.SetActive(true);
        break;
      }
    }

    private void DisableAllPages() {
      mainMenuContainer.SetActive(false);
      mapContainer.SetActive(false);
      gamesContainer.SetActive(false);
    }
}
