using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer, mapContainer, gamesContainer, dealsContainer, yourProfileContainer, businessContainer, notificationsContainer, indevContainer, menuContainer;
    [SerializeField] private GameObject uiBackground, notificationTopCover;
    private string previousPage, currentPage;
    [HideInInspector] public bool filterBusinessDeals = false, filterBusinessGames = false;
    [SerializeField] private GameObject businessButton;

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
        uiBackground.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePage(previousPage);
        }
        if (CrossSceneVariables.Instance.isBusiness && CrossSceneVariables.Instance.activeSubscription)
        {
            businessButton.SetActive(true);
        }
        else
        {
            businessButton.SetActive(false);
        }
    }

    public void ChangePage(string pageToOpen)
    {
        DisableAllPages();
        switch (pageToOpen.Substring(0, pageToOpen.Length - 1))
        {
            case "MainMenu":
                previousPage = currentPage;
                currentPage = "MainMenu";
                mainMenuContainer.SetActive(true);
                uiBackground.SetActive(false);
                break;

            case "Deals":
                previousPage = currentPage;
                currentPage = "Deals";
                if (pageToOpen.Substring(pageToOpen.Length - 1) == "B")
                {
                    filterBusinessDeals = true;
                }
                else
                {
                    filterBusinessDeals = false;
                }
                dealsContainer.SetActive(true);
                break;

            case "Games":
                previousPage = currentPage;
                currentPage = "Games";
                if (pageToOpen.Substring(pageToOpen.Length - 1) == "B")
                {
                    filterBusinessGames = true;
                }
                else
                {
                    filterBusinessGames = false;
                }
                gamesContainer.SetActive(true);
                break;

            case "Map":
                previousPage = currentPage;
                currentPage = "Map";
                mapContainer.SetActive(true);
                break;

            case "YourProfile":
                previousPage = currentPage;
                currentPage = "YourProfile";
                yourProfileContainer.SetActive(true);
                break;

            case "Business":
                previousPage = currentPage;
                currentPage = "Business";
                businessContainer.SetActive(true);
                uiBackground.SetActive(false);
                break;
            case "Notifications":
                print("af");
                previousPage = currentPage;
                currentPage = "Notifications";
                notificationsContainer.SetActive(true);
                notificationTopCover.SetActive(true);
                uiBackground.SetActive(false);
                break;
            case "Indev":
                previousPage = currentPage;
                currentPage = "Indev";
                indevContainer.SetActive(true);
                break;
            case "DemoMenu":
                previousPage = currentPage;
                currentPage = "DemoMenu";
                menuContainer.SetActive(true);
                break;
        }
    }

    private void DisableAllPages()
    {
        mainMenuContainer.SetActive(false);
        mapContainer.SetActive(false);
        gamesContainer.SetActive(false);
        dealsContainer.SetActive(false);
        yourProfileContainer.SetActive(false);
        businessContainer.SetActive(false);
        notificationsContainer.SetActive(false);
        notificationTopCover.SetActive(false);
        indevContainer.SetActive(false);
        menuContainer.SetActive(false);
        uiBackground.SetActive(true);
    }
    public void GlobalBack()
    {
        switch (currentPage)
        {
            case "MainMenu":

                break;
            case "Deals":
                //dealsContainer.GetComponent<DealsController>().GlobalBack();
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

    public void Logout()
    {
        Destroy(GameObject.Find("_DONTDESTROYONLOAD"));
        PlayerPrefs.SetInt("stayloggedin", 0);
        PlayerPrefs.SetString("email", "");
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
