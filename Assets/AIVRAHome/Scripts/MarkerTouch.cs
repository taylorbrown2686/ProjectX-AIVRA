using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerTouch : MonoBehaviour
{
    private LocationInfoPopulator populator;
    public string lastTouchedMarkerAddress;
    private MarkerCreator markerCreator;
    [SerializeField] private Text addressToNavText;

    void Start() {
      populator = GameObject.Find("MainContent").GetComponent<LocationInfoPopulator>();
      OnlineMaps map = OnlineMaps.instance;
      markerCreator = this.GetComponent<MarkerCreator>();
      foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance) {
        marker.OnClick += OnMarkerClick;
      }
    }

    private void OnMarkerClick(OnlineMapsMarkerBase marker) {
      if (markerCreator.MapType == "Game") {
        addressToNavText = GameObject.Find("TouchedMarkerAddress").GetComponent<Text>();
        lastTouchedMarkerAddress = populator.GetLocationAddress(marker.label);
        addressToNavText.text = lastTouchedMarkerAddress;
      }
      //lastTouchedMarkerAddress = populator.PopulateLocationInfo(marker.label);
    }

    public void RefreshOnclickListeners() {
      foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance) {
        marker.OnClick += OnMarkerClick;
      }
    }
}
