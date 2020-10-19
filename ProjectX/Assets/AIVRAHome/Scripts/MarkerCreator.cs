using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attached to Map
public class MarkerCreator : MonoBehaviour
{
    [SerializeField] private Texture2D[] markers;
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    private bool updatePlayer = true;

    IEnumerator Start() {
      yield return new WaitForSeconds(0.25f); //Gives MarkerData time to initialize
      PlaceMarkersOnMap();
      PlacePlayerMarker();
    }

    void Update() {
      if (updatePlayer) {
        StartCoroutine(PlacePlayerMarker());
      }
    }

    private void PlaceMarkersOnMap() {
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        markerManager.Create(new Vector2(data.Longitude, data.Latitude),
          markers[GetMarkerFromName(data.HostedEvent.EventType)],
          data.UID.ToString());
      }
      this.gameObject.AddComponent<MarkerTouch>();
    }

    private IEnumerator PlacePlayerMarker() {
      updatePlayer = false;
      markerManager.RemoveMarkerByLabel("You");
      markerManager.Create(new Vector2(PlayerCoordinates.Instance.PlayerLat, PlayerCoordinates.Instance.PlayerLng),
        markers[3], "You");
      yield return new WaitForSeconds(5f);
      updatePlayer = true;  
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
