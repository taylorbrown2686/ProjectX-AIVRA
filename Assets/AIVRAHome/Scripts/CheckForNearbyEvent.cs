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
    private int recentUID;
    public GameLockStatus gameLockController;


    void Update() {
      if (canCheckProx && Input.location.status == LocationServiceStatus.Running) {
        if (!inLocation) {
          StartCoroutine(CheckProximityToEventCoords());
        } else {
          //check if they leave
          StartCoroutine(CheckProximityToCurrentLocation());
        }
      }
    }

    private IEnumerator CheckProximityToEventCoords() {
      canCheckProx = false;
      float playerLat = PlayerCoordinates.Instance.PlayerLat;
      float playerLng = PlayerCoordinates.Instance.PlayerLng;
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLat, playerLng),
          new Vector2(data.Latitude, data.Longitude)).magnitude < data.HostedEvents[0].EventRadius && recentUID != data.UID) {
            inLocation = true;
            recentUID = data.UID;
            string[] hostedGameTitles = new string[data.HostedEvents.Length];
            for (int i = 0; i < data.HostedEvents.Length; i++) {
              hostedGameTitles[i] = data.HostedEvents[i].EventTitle;
            }
            gameLockController.UnlockGames(hostedGameTitles);
            //data.OnRadiusEnter.Invoke();
            aivraSays.StartCoroutine(aivraSays.Say("You are near an event! " + data.HostedEvents[0].EventType + ": " + data.HostedEvents[0].EventTitle));
        }
      }
      yield return new WaitForSeconds(5f);
      canCheckProx = true;
    }

    private IEnumerator CheckProximityToCurrentLocation() {
      canCheckProx = false;
      float playerLat = PlayerCoordinates.Instance.PlayerLat;
      float playerLng = PlayerCoordinates.Instance.PlayerLng;
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        if (data.UID == recentUID) {
          if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLat, playerLng),
            new Vector2(data.Latitude, data.Longitude)).magnitude > data.HostedEvents[0].EventRadius) {
              inLocation = false;
              recentUID = 0;
              gameLockController.LockGames();
              //data.OnRadiusExit.Invoke();
              aivraSays.StartCoroutine(aivraSays.Say("You are leaving an event area!"));
          }
        }
      }
      yield return new WaitForSeconds(5f);
      canCheckProx = true;
    }

}
