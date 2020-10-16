using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARLocation;
using static OnlineMapsUtils;

public class CorrectGPS : MonoBehaviour
{
    /*private float estimatedLat, estimatedLng;
    private float trueLat, trueLng;
    public float TrueLat {get {return trueLat;} set {trueLat = value;}} //Properties for lat/lng
    public float TrueLng {get {return trueLng;} set {trueLng = value;}}
    public GameObject confirmLocationPopup;
    public AIVRACustomProvider locationProvider; //ARLocationRoot
    public OnlineMapsMarkerManager markerManager;
    public AdjustMap mapMove;

    //DEBUG
    public Text infoText;

    void Start() {
      confirmLocationPopup.SetActive(false); //Turn off popup by default
      locationProvider = new AIVRACustomProvider();
      GameObject.Find("ARLocationRoot").GetComponent<ARLocationProvider>().Provider = locationProvider;
    }

    void Update() {
      estimatedLat = Input.location.lastData.latitude; //Set our estimated position to compare to
      estimatedLng = Input.location.lastData.longitude;
    }

    public void ConfirmLocation() { //Onclick listener
      confirmLocationPopup.SetActive(true); //Turn on the popup
    }

    public void SetGPS() { //Confirm button on popup
      //Math to move ARLocationRoot
      /*double bearing = Mathf.Tan((trueLat - estimatedLat) / (trueLng - estimatedLng));
      double distance = Mathf.Sqrt(Mathf.Pow(trueLat - estimatedLat, 2) + Mathf.Pow(trueLng - estimatedLng, 2));
      double radBearing = bearing * Mathf.Deg2Rad;
      double distanceToMeters = distance * 111111f;
      Vector3 newPos = new Vector3(Convert.ToSingle(Math.Cos(radBearing)), 0, Convert.ToSingle(Math.Sin(radBearing)));
      newPos *= Convert.ToSingle(distanceToMeters); //change back to float so Unity can use the value (internally uses floats)
      arlr.transform.position = newPos;
      //infoText.text = Input.location.lastData.latitude + "," + Input.location.lastData.longitude + "\n" + trueLat + "," + trueLng + "\n" + arlr.transform.position + "\n" + radBearing + "\n" + distanceToMeters;
      Debug.Log(locationProvider.Name);
      locationProvider.SetSpoofedLocation(trueLat, trueLng);
      confirmLocationPopup.SetActive(false);
      //remove portals
      markerManager.RemoveMarkerByLabel("Portal");
      //remove text

      //move map
      mapMove.StartCoroutine(mapMove.MoveMap());
    }

    public void Cancel() { //Cancel button on popup
      confirmLocationPopup.SetActive(false);
    }*/
}
