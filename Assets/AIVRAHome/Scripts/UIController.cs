using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer, mapContainer, gamesContainer, dealsContainer, businessContainer;
    private string previousPage, currentPage;

    void Start() {
      DisableAllPages();
      mainMenuContainer.SetActive(true);
      currentPage = "MainMenu";
    }

    void Update () {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        ChangePage(previousPage);
      }
    }

    public void ChangePage(string pageToOpen) {
      DisableAllPages();
      switch (pageToOpen) {
        case "MainMenu":
          previousPage = currentPage;
          currentPage = "MainMenu";
          mainMenuContainer.SetActive(true);
        break;

        case "Deals":
          previousPage = currentPage;
          currentPage = "Deals";
          dealsContainer.SetActive(true);
        break;

        case "Games":
          previousPage = currentPage;
          currentPage = "Games";
          gamesContainer.SetActive(true);
        break;

        case "Entertainment":
          previousPage = currentPage;
          currentPage = "Entertainment";
          gamesContainer.SetActive(true);
        break;

        case "Business":
          previousPage = currentPage;
          currentPage = "Business";
          businessContainer.SetActive(true);
        break;
      }
    }

    private void DisableAllPages() {
      mainMenuContainer.SetActive(false);
      mapContainer.SetActive(false);
      gamesContainer.SetActive(false);
      dealsContainer.SetActive(false);
      businessContainer.SetActive(false);
    }
}
