using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessController : MonoBehaviour
{
    [SerializeField] private GameObject scanDealsScreen, manageBusinessScreen, optionSelectScreen;
    public string businessName, businessAddress;
    public string businessLat, businessLng;
    public Deal[] deals;
    public Reward[] rewards;
    public HostedGame[] hostedGames;
    public HostedEvent[] hostedEvents;

    void Start()
    {
        //StartCoroutine(GetBusinessInfo());
        //StartCoroutine(GetDeals());
        //StartCoroutine(GetRewards());
        //StartCoroutine(GetHostedGames());
        //StartCoroutine(GetHostedEvents());
    }

    public void GoToScanDeals()
    {
        optionSelectScreen.SetActive(false);
        scanDealsScreen.SetActive(true);
    }

    public void GoToManageBusiness()
    {
        optionSelectScreen.SetActive(false);
        manageBusinessScreen.SetActive(true);
    }

    public void GlobalBack()
    {
        scanDealsScreen.SetActive(false);
        manageBusinessScreen.SetActive(false);
        optionSelectScreen.SetActive(true);
    }

    private IEnumerator GetBusinessInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/getBusinessNameAndAddress.php", form);
        yield return www;
        string[] stringToSplit = www.text.Split('$');
        businessName = stringToSplit[0];
        businessAddress = stringToSplit[1];
        businessLat = stringToSplit[2];
        businessLng = stringToSplit[3];
    }

    /*private IEnumerator GetDeals()
    {

    }

    private IEnumerator GetRewards()
    {

    }

    private IEnumerator GetHostedGames()
    {

    }

    private IEnumerator GetHostedEvents()
    {

    }*/
}
