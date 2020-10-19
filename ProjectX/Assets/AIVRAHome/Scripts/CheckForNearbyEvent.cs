using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OnlineMapsUtils;

public class CheckForNearbyEvent : MonoBehaviour
{
    public AIVRASays aivraSays;
    private bool canCheckProx = true;
    private int recentUID;

    void Update() {
      if (canCheckProx) {
        StartCoroutine(CheckProximityToEventCoords());
      }
    }

    private IEnumerator CheckProximityToEventCoords() {
      canCheckProx = false;
      float playerLat = PlayerCoordinates.Instance.PlayerLat;
      float playerLng = PlayerCoordinates.Instance.PlayerLng;
      /*foreach (KeyValuePair<Vector2, int> entry in EventCoordinates.Instance.coordsWithUID) {
        if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLat, playerLng), entry.Key).magnitude < 0.01f) {
          foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
            if (entry.Value == data.UID) {
              aivraSays.StartCoroutine(
                aivraSays.Say("You are near an event! " + data.HostedEvent.EventType + ": " + data.HostedEvent.EventTitle));
            }
          }
        }
      }*/
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        if (OnlineMapsUtils.DistanceBetweenPoints(new Vector2(playerLat, playerLng),
          new Vector2(data.Latitude, data.Longitude)).magnitude < 0.01f && recentUID != data.UID) {
            recentUID = data.UID;
            aivraSays.StartCoroutine(
              aivraSays.Say("You are near an event! " + data.HostedEvent.EventType + ": " + data.HostedEvent.EventTitle));
        }
      }
      yield return new WaitForSeconds(5f);
      canCheckProx = true;
    }

}
