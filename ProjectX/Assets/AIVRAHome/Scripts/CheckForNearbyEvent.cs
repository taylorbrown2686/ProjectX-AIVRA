using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OnlineMapsUtils;

public class CheckForNearbyEvent : MonoBehaviour
{
    public AIVRASays aivraSays;
    private bool canCheckProx = true;
    private bool inLocation = false;
    private Vector2 currentLocationCoords;
    private string currentBusinessName;
    public GameLockStatus gameLockController;
    public GameObject aivraSaysPulldown;

    void Update() {
      if (canCheckProx && Input.location.status == LocationServiceStatus.Running) {
        if (!inLocation) {
          StartCoroutine(CheckProximityToEventCoords());
          aivraSaysPulldown.SetActive(false);
        } else {
          //check if they leave
          StartCoroutine(CheckProximityToCurrentLocation());
          aivraSaysPulldown.SetActive(true);
        }
      }
    }

    private IEnumerator CheckProximityToEventCoords() {
      canCheckProx = false;
      float playerLat = PlayerCoordinates.Instance.PlayerLat;
      float playerLng = PlayerCoordinates.Instance.PlayerLng;
      foreach (KeyValuePair<string, Vector2> coord in CrossSceneVariables.Instance.nearbyBusinessCoords) {
        if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLng, playerLat), coord.Value).magnitude < 0.035f) {
            //gameLockController.UnlockGames(hostedGameTitles);
            inLocation = true;
            //aivraSaysPulldown.SetActive(true);
            aivraSays.StartCoroutine(aivraSays.Say("You are at: " + coord.Key));
            currentLocationCoords = coord.Value;
            currentBusinessName = coord.Key;
            StartCoroutine(GetLocationGames());
        }
      }
      yield return new WaitForSeconds(5f);
      canCheckProx = true;
    }

    private IEnumerator CheckProximityToCurrentLocation() {
      canCheckProx = false;
      float playerLat = PlayerCoordinates.Instance.PlayerLat;
      float playerLng = PlayerCoordinates.Instance.PlayerLng;
      if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLng, playerLat), currentLocationCoords).magnitude > 0.035f) {
        inLocation = false;
       // aivraSaysPulldown.SetActive(false);
        //gameLockController.LockGames();
        aivraSays.StartCoroutine(aivraSays.Say("You are leaving " + currentBusinessName + ". Come back soon!"));
        currentLocationCoords = new Vector2(0,0);
        currentBusinessName = "";
        gameLockController.LockGames();
      }
      yield return new WaitForSeconds(5f);
      canCheckProx = true;
    }

    private IEnumerator GetLocationGames() {
        WWWForm form = new WWWForm();
        form.AddField("businessName", currentBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getUnlockedGamesForBusiness.php", form);
        yield return www;
        string[] splitString = www.text.Split('&');
        Array.Resize(ref splitString, splitString.Length - 1);
        gameLockController.UnlockGames(splitString);
    }

}
