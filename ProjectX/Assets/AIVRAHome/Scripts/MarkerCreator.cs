using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attached to Map
public class MarkerCreator : MonoBehaviour
{
    [SerializeField] private string mapType;
    [SerializeField] private Texture2D[] markers;
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    private bool updatePlayer = true;
    private string lastEventTitleFilter;

    public string MapType {get => mapType;}

    IEnumerator Start() {
      yield return new WaitForSeconds(0.25f); //Gives MarkerData time to initialize
    }

    void Update() {
      if (updatePlayer) {
        StartCoroutine(PlacePlayerMarker());
      }
    }

    public void ResetMarkersWithLastFilter() {
      StartCoroutine(PlaceMarkersOnMap(lastEventTitleFilter));
    }

    public IEnumerator PlaceMarkersOnMap(string eventTitleFilter) {
      lastEventTitleFilter = eventTitleFilter;
      markerManager.RemoveAll();
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        foreach (EventAtLocation eventAL in data.HostedEvents) {
          if (eventAL.EventType == mapType) {
            if (eventAL.EventTitle == eventTitleFilter) {
              while (markerManager.IsInstanceNull() == true) {
                yield return new WaitForSeconds(0.01f);
              }
              OnlineMapsMarker marker = markerManager.Create(new Vector2(data.Longitude, data.Latitude),
                markers[GetMarkerFromName(eventAL.EventTitle)],
                data.UID.ToString());
              OnlineMaps map = this.GetComponent<OnlineMaps>();
              marker.originalRadius = eventAL.EventRadius;
              marker.scale = eventAL.EventRadius / GetScaleFactor(map.zoom); //Scale of marker with 1 size at 22 zoom
            }
          }
        }
      }
      if (this.gameObject.GetComponent<MarkerTouch>() == null) {
        this.gameObject.AddComponent<MarkerTouch>();
      } else {
        this.gameObject.GetComponent<MarkerTouch>().RefreshOnclickListeners();
      }
    }

    private IEnumerator PlacePlayerMarker() {
      updatePlayer = false;
      markerManager.RemoveMarkerByLabel("You");
      markerManager.Create(new Vector2(PlayerCoordinates.Instance.PlayerLng, PlayerCoordinates.Instance.PlayerLat),
        markers[3], "You");
      //this.gameObject.GetComponent<OnlineMaps>().SetPosition(PlayerCoordinates.Instance.PlayerLng, PlayerCoordinates.Instance.PlayerLat);
      yield return new WaitForSeconds(5f);
      updatePlayer = true;
    }

    private int GetMarkerFromName(string name) {
      switch (name) {
        case "Ghosts in the Graveyard":
          return 0;
        break;

        case "HuntAR":
          return 1;
        break;

        case "Bar Dice":
          return 2;
        break;

        case "AR Tetris":
          return 3;
        break;

        case "AR Fishin'":
          return 4;
        break;

        default:
          return -1;
        break;
      }
    }

    private float GetScaleFactor(float zoom) {
      return 0.0008f * Mathf.Pow(2, 22 - zoom);
    }
}
