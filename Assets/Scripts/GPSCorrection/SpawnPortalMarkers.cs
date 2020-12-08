using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script spawns portals at a set of coordinates given by the AI Cloud Server
public class SpawnPortalMarkers : MonoBehaviour
{
    public OnlineMapsMarkerManager markerManager;

    public void SpawnPortalsAtCoordinates(string[] coordinates) {
      for (int i = 0; i < coordinates.Length; i++) { //Create a marker for each coordinate
        if (i % 2 != 0) {
          markerManager.Create(
            new Vector2(float.Parse(coordinates[i]), float.Parse(coordinates[i-1])),
            markerManager.markerImages.markerTextures[3], "Portal");
        }
      }
      this.gameObject.AddComponent<GetCoordsFromMarker>(); //Add the get coordinate class to the Map gameObject
    }
}
