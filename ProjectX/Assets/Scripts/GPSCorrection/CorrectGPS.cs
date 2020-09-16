using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OnlineMapsUtils;

public class CorrectGPS : MonoBehaviour
{
    private float estimatedLat, estimatedLng;
    private float trueLat, trueLng;
    public float TrueLat {get {return trueLat;} set {trueLat = value;}} //Properties for lat/lng
    public float TrueLng {get {return trueLng;} set {trueLng = value;}}
    public GameObject confirmLocationPopup;
    public GameObject arlr; //ARLocationRoot

    //DEBUG
    public Text infoText;

    void Start() {
      confirmLocationPopup.SetActive(false); //Turn off popup by default
    }

    void Update() {
      //estimatedLat = Input.location.lastData.latitude; //Set our estimated position to compare to
      //estimatedLng = Input.location.lastData.longitude;
    }

    public void ConfirmLocation() { //Onclick listener
      confirmLocationPopup.SetActive(true); //Turn on the popup
    }

    public void SetGPS() { //Confirm button on popup
      //Math to move ARLocationRoot
      double bearing = OnlineMapsUtils.Angle2D(estimatedLat, estimatedLng, trueLat, trueLng); //returns as type double
      double distance = OnlineMapsUtils.DistanceBetweenPoints(new Vector2(trueLat, trueLng), new Vector2(estimatedLat, estimatedLng)).magnitude;
      double radBearing = bearing * Mathf.Deg2Rad;
      double distanceToMeters = distance * 1000f;
      Vector3 newPos = new Vector3(Convert.ToSingle(Math.Cos(radBearing)), 0, Convert.ToSingle(Math.Sin(radBearing)));
      newPos *= Convert.ToSingle(distanceToMeters); //change back to float so Unity can use the value (internally uses floats)
      arlr.transform.position = newPos;
      infoText.text = estimatedLat + "," + estimatedLng + "\n" + trueLat + "," + trueLng + "\n" + arlr.transform.position;
    }

    public void Cancel() { //Cancel button on popup
      confirmLocationPopup.SetActive(false);
    }
}
