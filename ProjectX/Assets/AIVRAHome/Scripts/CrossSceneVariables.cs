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
    public bool? isBusiness = null;

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

    private IEnumerator GetNearbyBusinessCoords() {
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCoordinatesFromNearbyBusinesses.php");
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i += 3) {
            nearbyBusinessCoords.Add(splitString[i], new Vector2(float.Parse(splitString[i + 1]), float.Parse(splitString[i + 2])));
        }
    }

    private IEnumerator CheckBusinessStatus()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
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
        }
    }

    private IEnumerator GetUsernameAndName()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getUsernameAndNameFromEmail.php", form);
        yield return www;
        Debug.Log(www.text);
        string[] splitString = www.text.Split('#');
        username = splitString[1];
        name = splitString[0];
    }

    void OnMainMenuSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(GetNearbyBusinessCoords());
        StartCoroutine(CheckBusinessStatus());
        StartCoroutine(GetUsernameAndName());
    }

}
