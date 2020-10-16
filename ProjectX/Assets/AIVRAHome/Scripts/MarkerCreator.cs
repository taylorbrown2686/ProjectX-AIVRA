using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCreator : MonoBehaviour
{
    [SerializeField] private Texture2D[] markers;
    [SerializeField] private OnlineMapsMarkerManager markerManager;

    IEnumerator Start() {
      yield return new WaitForSeconds(0.25f); //Gives MarkerData time to initialize
      PlaceMarkersOnMap();
    }

    private void PlaceMarkersOnMap() {
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        markerManager.Create(new Vector2(data.Longitude, data.Latitude),
          markers[GetMarkerFromName(data.HostedEvent.EventType)],
          data.UID.ToString());
      }
      this.gameObject.AddComponent<MarkerTouch>();
    }

    private int GetMarkerFromName(string name) {
      switch (name) {
        case "Game":
          return 0;
        break;

        case "Experience":
          return 1;
        break;

        case "Deal":
          return 2;
        break;

        default:
          return -1;
        break;
      }
    }
}
