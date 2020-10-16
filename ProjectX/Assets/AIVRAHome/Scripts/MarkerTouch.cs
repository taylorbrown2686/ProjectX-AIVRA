using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTouch : MonoBehaviour
{
    [SerializeField] private LocationInfoPopulator populator;

    void Start() {
      populator = GameObject.Find("LocationInfoContainer").GetComponent<LocationInfoPopulator>();
      OnlineMaps map = OnlineMaps.instance;
      foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance) {
        marker.OnClick += OnMarkerClick;
      }
    }

    private void OnMarkerClick(OnlineMapsMarkerBase marker) {
      populator.PopulateLocationInfo(marker.label);
    }
}
