using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossSceneVariables : MonoBehaviour
{
    public string email;
    public string username;
    public string name = "";
    public Dictionary<string, Vector2> nearbyBusinessCoords = new Dictionary<string, Vector2>();
    public List<string> businessesInRadius = new List<string>();
    public bool isBusiness;
    public bool activeSubscription = false;
    public bool inBusiness = false;
    public bool newNotifications = false;
    private bool canUpdateNotifications = true;
    public string inBusinessName;

    [SerializeField] private GameObject createAvatarBlock, subscribeBusinessBlock;

    private static CrossSceneVariables instance = null;

    public static CrossSceneVariables Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnMainMenuSceneLoaded;
    }

    private void Update()
    {
        if (canUpdateNotifications)
        {
            StartCoroutine(CheckForNotifications());
        }
    }

    private IEnumerator GetNearbyBusinessCoords()
    {
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCoordinatesFromNearbyBusinesses.php");
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i += 3)
        {
            nearbyBusinessCoords.Add(splitString[i], new Vector2(float.Parse(splitString[i + 1]), float.Parse(splitString[i + 2])));
        }
    }

    private IEnumerator CheckBusinessStatus()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkForBusinessAccount.php", form);
        yield return www;
        if (www.text == "0")
        {
            //not a business
            isBusiness = false;
        }
        else
        {
            //is business
            isBusiness = true;
            StartCoroutine(CheckForActiveSubscription());
        }
    }

    private IEnumerator GetUsernameAndName()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getUsernameAndNameFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        name = splitString[0];
        username = splitString[1];
    }

    private IEnumerator CheckForAvatar()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkIfUserHasAvatar.php", form);
        yield return www;
        if (www.text == "0")
        {
            GameObject newObj = Instantiate(createAvatarBlock, Vector3.zero, Quaternion.identity);
            newObj.transform.SetParent(GameObject.Find("MainUI").transform, false);
        }
        else
        {
            yield break;
        }
    }

    private IEnumerator CheckForActiveSubscription()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getSubscriptionStatusFromBusiness.php", form);
        yield return www;
        if (www.text == "0")
        {
            GameObject newObj = Instantiate(subscribeBusinessBlock, Vector3.zero, Quaternion.identity);
            newObj.transform.SetParent(GameObject.Find("MainUI").transform, false);
            activeSubscription = false;
        }
        else
        {
            activeSubscription = true;
            yield break;
        }
    }

    private IEnumerator CheckForNotifications()
    {
        canUpdateNotifications = false;
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getNotificationsFromEmail.php", form);
        yield return www;
        print(www.text);
        if (www.text != "None")
        {
            newNotifications = true;
        } else
        {
            newNotifications = false;
        }
        yield return new WaitForSeconds(15f);
        canUpdateNotifications = true;
    }

    public void PopulateBusinessesInRadius(float radius)
    {
        businessesInRadius.Clear();
        float playerLat = Input.location.lastData.latitude;
        float playerLng = Input.location.lastData.longitude;
        foreach (KeyValuePair<string, Vector2> pair in nearbyBusinessCoords)
        {
            if (radius == 50)
            {
                businessesInRadius.Add(pair.Key);
                continue;
            }
            if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLng, playerLat), pair.Value).magnitude < radius)
            {
                businessesInRadius.Add(pair.Key);
            }
        }
        NewDealsController.Instance.StartCoroutine(NewDealsController.Instance.GetSavedDeals());
    }

    void OnMainMenuSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "AIVRAHome")
        {
            StartCoroutine(GetNearbyBusinessCoords());
            StartCoroutine(CheckBusinessStatus());
            StartCoroutine(GetUsernameAndName());
            StartCoroutine(CheckForAvatar());
            StartCoroutine(CheckForNotifications());
        }
    }

}
