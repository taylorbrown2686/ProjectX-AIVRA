using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject dealFilterScrollView, rewardFilterScrollView, gameFilterScrollView, eventFilterScrollView, businessOverlay;
    [SerializeField] private Dropdown mapDropdownFilter;
    [SerializeField] private Text businessNameText, businessAddressText;
    [SerializeField] private RawImage businessImage;
    [SerializeField] private InputField searchInput;
    private int businessImageIndex = 0;
    [SerializeField] private List<Texture2D> businessImages = new List<Texture2D>();
    [SerializeField] private Dictionary<string, Vector2> dealsDict, rewardsDict, gamesDict, eventsDict, allDict;
    [SerializeField] private Texture2D[] markers;
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField] private OnlineMaps map;
    [SerializeField] private LockScrollView scrollViewFilterLock;
    private bool updatePlayer = true;

    private void Start()
    {
        dealFilterScrollView.SetActive(false);
        gameFilterScrollView.SetActive(false);
        eventFilterScrollView.SetActive(false);
        dealsDict = new Dictionary<string, Vector2>();
        rewardsDict = new Dictionary<string, Vector2>();
        gamesDict = new Dictionary<string, Vector2>();
        eventsDict = new Dictionary<string, Vector2>();
        allDict = new Dictionary<string, Vector2>();
    }

    void Update()
    {
        if (updatePlayer)
        {
            StartCoroutine(PlacePlayerMarker());
        }
    }

    public void OnFilterChanged(string mapTypeAndFilter) //Sorry, Unity can't handle 2 onclick arguments.
    {                                                    //Have to do a workaround since bad engine is bad
        string[] splitString = mapTypeAndFilter.Split('#');
        StartCoroutine(GetNameAndCoordsOfEvents(splitString[0], splitString[1]));
    }

    private IEnumerator GetNameAndCoordsOfEvents(string mapType, string optionalFilter)
    {
        WWWForm form = new WWWForm();
        form.AddField("maptype", mapType);
        if (optionalFilter != null)
        {
            form.AddField("optionalfilter", optionalFilter);
        }
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCoordinatesFromAnyEvent.php", form);
        yield return www;
        string[] splitString = www.text.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, Vector2> tempDict = new Dictionary<string, Vector2>();
        for (int i = 0; i < splitString.Length; i+=3)
        {
            tempDict.Add(splitString[i], new Vector2(Convert.ToSingle(splitString[i + 1]), Convert.ToSingle(splitString[i + 2])));
        }
        switch (mapType)
        {
            case "deals":
                dealsDict = tempDict;
                StartCoroutine(PlaceMarkersOnMap("deals"));
                break;
            case "rewards":
                rewardsDict = tempDict;
                StartCoroutine(PlaceMarkersOnMap("rewards"));
                break;
            case "hosted_games":
                gamesDict = tempDict;
                StartCoroutine(PlaceMarkersOnMap("hosted_games"));
                break;
            case "hosted_events":
                eventsDict = tempDict;
                StartCoroutine(PlaceMarkersOnMap("hosted_events"));
                break;
            case "all":
                allDict = tempDict;
                StartCoroutine(PlaceMarkersOnMap("all"));
                break;
        }
    }

    public void ChangeMapType()
    {
        switch (mapDropdownFilter.options[mapDropdownFilter.value].text)
        {
            case "Deals":
                dealFilterScrollView.SetActive(true);
                rewardFilterScrollView.SetActive(false);
                gameFilterScrollView.SetActive(false);
                eventFilterScrollView.SetActive(false);
                scrollViewFilterLock.ChangeItemCount(9);
                StartCoroutine(GetNameAndCoordsOfEvents("deals", "All"));
                break;
            case "Rewards":
                rewardFilterScrollView.SetActive(true);
                gameFilterScrollView.SetActive(false);
                dealFilterScrollView.SetActive(false);
                eventFilterScrollView.SetActive(false);
                scrollViewFilterLock.ChangeItemCount(5);
                StartCoroutine(GetNameAndCoordsOfEvents("rewards", "All"));
                break;
            case "Games":
                gameFilterScrollView.SetActive(true);
                dealFilterScrollView.SetActive(false);
                rewardFilterScrollView.SetActive(false);
                eventFilterScrollView.SetActive(false);
                scrollViewFilterLock.ChangeItemCount(5);
                StartCoroutine(GetNameAndCoordsOfEvents("hosted_games", "All"));
                break;
            case "Events":
                eventFilterScrollView.SetActive(true);
                rewardFilterScrollView.SetActive(false);
                dealFilterScrollView.SetActive(false);
                gameFilterScrollView.SetActive(false);
                scrollViewFilterLock.ChangeItemCount(15);
                StartCoroutine(GetNameAndCoordsOfEvents("hosted_events", "All"));
                break;
            case "All":
                dealFilterScrollView.SetActive(false);
                rewardFilterScrollView.SetActive(false);
                gameFilterScrollView.SetActive(false);
                eventFilterScrollView.SetActive(false);
                StartCoroutine(GetNameAndCoordsOfEvents("all", "All"));
                break;
        }
    }

    private IEnumerator PlacePlayerMarker()
    {
        updatePlayer = false;
        markerManager.RemoveMarkerByLabel("You");
        markerManager.Create(new Vector2(PlayerCoordinates.Instance.PlayerLng, PlayerCoordinates.Instance.PlayerLat),
          markers[3], "You");
        //this.gameObject.GetComponent<OnlineMaps>().SetPosition(PlayerCoordinates.Instance.PlayerLng, PlayerCoordinates.Instance.PlayerLat);
        yield return new WaitForSeconds(5f);
        updatePlayer = true;
    }

    public IEnumerator PlaceMarkersOnMap(string eventTypeFilter)
    {
        markerManager.RemoveAll();
        foreach (KeyValuePair<string, Vector2> pair in GetDictFromEventFilter(eventTypeFilter))
        {
            while (markerManager.IsInstanceNull() == true)
            {
                yield return new WaitForSeconds(0.01f);
            }
            if (searchInput.text != "")
            {
                if (!pair.Key.Contains(searchInput.text))
                {
                    continue;
                }
            }
            OnlineMapsMarker marker = markerManager.Create(new Vector2(pair.Value.y, pair.Value.x),
              markers[GetMarkerFromName(eventTypeFilter)], pair.Key);
            marker.originalRadius = 1;
            marker.scale = GetScaleFactor();
        }
        if (this.gameObject.GetComponent<MarkerTouch>() == null)
        {
            MarkerTouch mt = this.gameObject.AddComponent<MarkerTouch>();
            mt.mapController = this;
        }
        else
        {
            this.gameObject.GetComponent<MarkerTouch>().RefreshOnclickListeners();
        }
    }

    public void ViewBusinessOverlay(string businessName)
    {
        businessOverlay.SetActive(true);
        StartCoroutine(PopulateBusinessOverlay(businessName));
    }

    private IEnumerator PopulateBusinessOverlay(string businessName)
    {
        WWWForm form = new WWWForm();
        form.AddField("businessName", businessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getBusinessAddressAndImagesFromName.php", form);
        yield return www;
        string[] splitString = www.text.Split(new string[] { "STRING_SPLIT" }, System.StringSplitOptions.RemoveEmptyEntries);
        businessNameText.text = businessName;
        businessAddressText.text = splitString[0];
        for (int i = 1; i < splitString.Length; i++)
        {
            Texture2D image = new Texture2D(1, 1);
            image.LoadImage(System.Convert.FromBase64String(splitString[i]));
            businessImages.Add(image);
        }
        businessImage.texture = businessImages[0];
    }

    public void ChangeBusinessImage()
    {
        businessImageIndex += 1;
        if (businessImageIndex > businessImages.Count - 1)
        {
            businessImageIndex = 0;
        } else if (businessImageIndex < 0)
        {
            businessImageIndex = businessImages.Count;
        }
        businessImage.texture = businessImages[businessImageIndex];
    }

    public void CloseBusinessOverlay()
    {
        businessOverlay.SetActive(false);
    }

    private Dictionary<string, Vector2> GetDictFromEventFilter(string eventTypeFilter)
    {
        switch (eventTypeFilter)
        {
            case "deals":
                return dealsDict;
            case "rewards":
                return rewardsDict;
            case "hosted_games":
                return gamesDict;
            case "hosted_events":
                return eventsDict;
            case "all":
                return allDict;
        }
        return null; //unreachable
    }

    private int GetMarkerFromName(string eventTypeFilter)
    {
        switch (eventTypeFilter)
        {
            case "deals":
                return 0;
            case "rewards":
                return 1;
            case "hosted_games":
                return 2;
            case "hosted_events":
                return 3;
            case "all":
                return 4;
                
        }
        return -1; //unreachable
    }

    private float GetScaleFactor()
    {
        if (map.zoom < 18)
        {
            return 2;
        }
        return Convert.ToSingle((1.40625f * Math.Pow(map.zoom, 2)) - (51.5625f * map.zoom) + 473.75f);
    }

    public void OnSearch()
    {
        ChangeMapType();
    }
}
