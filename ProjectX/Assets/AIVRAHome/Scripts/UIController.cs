using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer, mapContainer, gamesContainer, dealsContainer, businessContainer, appOptions;
    private string previousPage, currentPage;

    private static UIController instance = null;

    public static UIController Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DisableAllPages();
        mainMenuContainer.SetActive(true);
        currentPage = "MainMenu";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePage(previousPage);
        }
    }

    public void ChangePage(string pageToOpen)
    {
        DisableAllPages();
        switch (pageToOpen)
        {
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

    private void DisableAllPages()
    {
        mainMenuContainer.SetActive(false);
        mapContainer.SetActive(false);
        gamesContainer.SetActive(false);
        dealsContainer.SetActive(false);
        businessContainer.SetActive(false);
    }

    public void GlobalBack()
    {
        switch (currentPage)
        {
            case "MainMenu":

                break;
            case "Deals":
                dealsContainer.GetComponent<DealsController>().GlobalBack();
                break;
            case "Games":

                break;
            case "Entertainment":

                break;
            case "Business":
                businessContainer.GetComponent<BusinessController>().GlobalBack();
                break;
        }
    }

    public void AppOptionsToggle()
    {
        appOptions.SetActive(!appOptions.activeSelf);
    }

    public void Logout()
    {
        PlayerPrefs.SetInt("stayloggedin", 0);
        PlayerPrefs.SetString("email", "");
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
