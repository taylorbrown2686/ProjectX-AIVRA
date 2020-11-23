using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMarkersForEvents : MonoBehaviour
{
    [SerializeField] private string mapType;
    [SerializeField] private OnlineMapsMarkerManager markerManager;
    [SerializeField] private Texture2D[] markers;

    void OnEnable()
    {
        StartCoroutine(PlaceMarkers());
    }

    private IEnumerator PlaceMarkers()
    {
        WWWForm form = new WWWForm();
        form.AddField("maptype", mapType);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/getCoordinatesFromAnyEvent.php", form);
        yield return www;
        string[] splitString = www.text.Split('&');
        Array.Resize(ref splitString, splitString.Length - 1);
        while (markerManager.IsInstanceNull() == true)
        {
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(splitString.Length);
        for (int i = 0; i < splitString.Length; i+=2)
        {
            Debug.Log("Creating Marker");
            OnlineMapsMarker marker = markerManager.Create(
                new Vector2(float.Parse(splitString[i+1]), float.Parse(splitString[i])), markers[0], "999999");
            marker.originalRadius = 0.01f;
            marker.scale = 0.01f;
        }
    }
}
