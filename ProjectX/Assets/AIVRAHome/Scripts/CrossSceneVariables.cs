using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneVariables : MonoBehaviour
{
    public string email;
    public Dictionary<string, Vector2> nearbyBusinessCoords = new Dictionary<string, Vector2>();

    private static CrossSceneVariables instance = null;

    public static CrossSceneVariables Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(GetNearbyBusinessCoords());
    }

    private IEnumerator GetNearbyBusinessCoords() {
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getCoordinatesFromNearbyBusinesses.php");
        yield return www;
        string[] splitString = www.text.Split('&');
        for (int i = 0; i < splitString.Length - 1; i+=3) {
            nearbyBusinessCoords.Add(splitString[i], new Vector2(float.Parse(splitString[i+1]), float.Parse(splitString[i+2])));
        }
    }

}
