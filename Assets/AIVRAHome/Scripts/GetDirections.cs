using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDirections : MonoBehaviour
{
      private string googleAPIKey = "AIzaSyBaYR5jsJKXg12zxgJ-OtND6rMQsYikjWM";
      public string yourAddress = null;

      public void GetAddressFromCoords() { //Reverse Geocoding
        string addressToNav = this.gameObject.GetComponent<MarkerTouch>().lastTouchedMarkerAddress;
        Vector2 yourCoords = new Vector2(PlayerCoordinates.Instance.PlayerLat, PlayerCoordinates.Instance.PlayerLng);
        GetLocationFromCoordinate getLocation = this.gameObject.AddComponent<GetLocationFromCoordinate>();
        getLocation.StartCoroutine(getLocation.GetLocationName(yourCoords, false));
        StartCoroutine(SendDirectionsRequest(addressToNav));
      }

      private IEnumerator SendDirectionsRequest(string addressToNav) {
        if (string.IsNullOrEmpty(googleAPIKey)) Debug.LogWarning("Please specify Google API Key");

        while (yourAddress == null || yourAddress == "") {
          yield return new WaitForSeconds(0.5f);
        }
        OnlineMapsGoogleDirections request = new OnlineMapsGoogleDirections(googleAPIKey, yourAddress, addressToNav);
        request.OnComplete += OnGoogleDirectionsComplete;
        request.Send();
      }

      private void OnGoogleDirectionsComplete(string response) {
        Debug.Log(response);
        // Try load result
        OnlineMapsGoogleDirectionsResult result = OnlineMapsGoogleDirections.GetResult(response);
        if (result == null || result.routes.Length == 0) return;
        // Get the first route
        OnlineMapsGoogleDirectionsResult.Route route = result.routes[0];
        // Draw route on the map
        OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(route.overview_polyline, Color.red, 3));
        // Calculate the distance
        int distance = route.legs.Sum(l => l.distance.value); // meters
        // Calculate the duration
        int duration = route.legs.Sum(l => l.duration.value); // seconds
        // Log distance and duration
        Debug.Log("Distance: " + distance + " meters, or " + (distance / 1000f).ToString("F2") + " km");
        Debug.Log("Duration: " + duration + " sec, or " + (duration / 60f).ToString("F1") + " min, or " + (duration / 3600f).ToString("F1") + " hours");
        yourAddress = null;
      }
}
