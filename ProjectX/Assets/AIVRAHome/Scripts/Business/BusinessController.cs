using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessController : MonoBehaviour
{
    [SerializeField] private GameObject scanDealsScreen, manageBusinessScreen, optionSelectScreen;
    public string businessName, businessAddress;
    public string businessLat, businessLng;
    public List<Deal> deals = new List<Deal>();
    public List<Reward> rewards = new List<Reward>();
    public List<HostedGame> hostedGames = new List<HostedGame>();
    public List<HostedEvent> hostedEvents = new List<HostedEvent>();
    public Dictionary<string, string> businessNamesAndAddresses = new Dictionary<string, string>();
    public Dictionary<string, string> businessCoordinates = new Dictionary<string, string>();

    void Start()
    {
        StartCoroutine(GetBusinessInfo());
        StartCoroutine(GetAdditionalLocations());
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

    public void RepullEvents()
    {
        StartCoroutine(GetDeals());
        StartCoroutine(GetRewards());
        StartCoroutine(GetHostedGames());
        StartCoroutine(GetHostedEvents());
    }

    private IEnumerator GetBusinessInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getBusinessNameAndAddress.php", form);
        yield return www;
        string[] stringToSplit = www.text.Split('$');
        businessName = stringToSplit[0];
        businessAddress = stringToSplit[1];
        businessLat = stringToSplit[2];
        businessLng = stringToSplit[3];
        businessNamesAndAddresses.Add(stringToSplit[0], stringToSplit[1]);
        businessCoordinates.Add(stringToSplit[0], stringToSplit[2] + "," + stringToSplit[3]);
        StartCoroutine(GetDeals());
        StartCoroutine(GetRewards());
        StartCoroutine(GetHostedGames());
        StartCoroutine(GetHostedEvents());
    }

    private IEnumerator GetAdditionalLocations()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getAdditionalBusinessNames.php", form);
        yield return www;
        string[] stringToSplit = www.text.Split('$');
        for (int i = 0; i < stringToSplit.Length - 1; i+=4)
        {
            businessNamesAndAddresses.Add(stringToSplit[i], stringToSplit[i + 1]);
            businessCoordinates.Add(stringToSplit[i], stringToSplit[i + 2] + "," + stringToSplit[i + 3]);
        }
    }

    private IEnumerator GetDeals() //Field Count: 10
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", businessName);
        form.AddField("queryType", "Deal");
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getEventListForBusiness.php", form);
        yield return www;
        Debug.Log(www.text);
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i+=10)
        {
            Deal deal = new Deal();
            deal.internalName = splitString[i];
            deal.externalName = splitString[i + 1];
            deal.category = splitString[i + 2];
            deal.tags = splitString[i + 3];
            deal.audience = splitString[i + 4];
            deal.discountType = splitString[i + 5];
            deal.discountAmount = splitString[i + 6];
            deal.expiry = splitString[i + 7];
            deal.amountDistributed = splitString[i + 8];
            deal.priceOfDiscountedItem = splitString[i + 9];
            deals.Add(deal);
        }
    }

    private IEnumerator GetRewards() //Field Count: 9
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", businessName);
        form.AddField("queryType", "Reward");
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getEventListForBusiness.php", form);
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i += 9)
        {
            Reward reward = new Reward();
            reward.internalName = splitString[i];
            reward.externalName = splitString[i + 1];
            reward.gameTitle = splitString[i + 2];
            reward.tags = splitString[i + 3];
            reward.typeOfReward = splitString[i + 4];
            reward.amountOrItem = splitString[i + 5];
            reward.requiredScore = splitString[i + 6];
            reward.expiry = splitString[i + 7];
            reward.amountDistributed = splitString[i + 8];
            rewards.Add(reward);
        }
    }

    private IEnumerator GetHostedGames() //Field Count: 6
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", businessName);
        form.AddField("queryType", "Game");
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getEventListForBusiness.php", form);
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            HostedGame hostedGame = new HostedGame();
            hostedGame.eventName = splitString[i];
            hostedGame.gameTitle = splitString[i + 1];
            hostedGame.tags = splitString[i + 2];
            hostedGame.desc = splitString[i + 3];
            hostedGame.subDesc = splitString[i + 4];
            hostedGame.endDate = splitString[i + 5];
            hostedGames.Add(hostedGame);
        }
    }

    private IEnumerator GetHostedEvents() //Field Count: 6
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", businessName);
        form.AddField("queryType", "Event");
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getEventListForBusiness.php", form);
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            HostedEvent hostedEvent = new HostedEvent();
            hostedEvent.eventName = splitString[i];
            hostedEvent.eventTitle = splitString[i + 1];
            hostedEvent.tags = splitString[i + 2];
            hostedEvent.desc = splitString[i + 3];
            hostedEvent.subDesc = splitString[i + 4];
            hostedEvent.endDate = splitString[i + 5];
            hostedEvents.Add(hostedEvent);
        }
    }
}
