using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script gets the coordinates of the touched marker
public class GetCoordsFromMarker : MonoBehaviour
{
    public CorrectGPS correction;

    private void Start()
    {
        OnlineMaps map = OnlineMaps.instance; //Get map instance
        correction = this.GetComponent<CorrectGPS>();

        // Add OnClick events to static markers
        foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance)
        {
            marker.OnClick += OnMarkerClick;
        }
    }

    private void OnMarkerClick(OnlineMapsMarkerBase marker)
    {
        // Show in console marker label.
        correction.ConfirmLocation(); //Turn on the popup
        correction.TrueLat = (float)marker.latitude; //Set the values to the lat/lng of the clicked marker
        correction.TrueLng = (float)marker.longitude;
    }
}
