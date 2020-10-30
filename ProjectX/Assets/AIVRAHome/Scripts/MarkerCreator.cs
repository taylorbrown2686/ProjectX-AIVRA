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

    public string MapType  {get => mapType;}

    IEnumerator Start() {
      yield return new WaitForSeconds(0.25f); //Gives MarkerData time to initialize
    }

    void Update() {
      if (updatePlayer) {
        StartCoroutine(PlacePlayerMarker());
      }
    }

    public void PlaceMarkersOnMap(string eventTitleFilter) {
      markerManager.RemoveAll();
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        foreach (EventAtLocation eventAL in data.HostedEvents) {
          if (eventAL.EventType == mapType) {
            if (eventAL.EventTitle == eventTitleFilter) {
              markerManager.Create(new Vector2(data.Longitude, data.Latitude),
                markers[GetMarkerFromName(eventAL.EventTitle)],
                data.UID.ToString());
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
      this.gameObject.GetComponent<OnlineMaps>().SetPosition(PlayerCoordinates.Instance.PlayerLng, PlayerCoordinates.Instance.PlayerLat);
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
}
