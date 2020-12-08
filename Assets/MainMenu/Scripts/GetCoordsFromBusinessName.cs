using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoordsFromBusinessName
{
    private string googleAPIKey = "AIzaSyBaYR5jsJKXg12zxgJ-OtND6rMQsYikjWM";
    public double lat, lng;

    public void SendRequest(string address) {
      Debug.Log("Starting");
      OnlineMapsGoogleGeocoding request = new OnlineMapsGoogleGeocoding(address, googleAPIKey);
      request.Send();
      request.OnComplete += OnRequestComplete;
    }

    private void OnRequestComplete(string s) {
        //A lot of string extracting needs to be done to get the coords.
        //We first pull the <location> tag from the response, then truncate that down to <lat> and <lng>
        int locationTagFrom = s.IndexOf("<location>") + "<location>".Length;
        int locationTagTo = s.LastIndexOf("</location>");
        string locationTag = s.Substring(locationTagFrom, locationTagTo - locationTagFrom);
        int latTagFrom = locationTag.IndexOf("<lat>") + "<lat>".Length;
        int latTagTo = locationTag.LastIndexOf("</lat>");
        lat = Convert.ToDouble(locationTag.Substring(latTagFrom, latTagTo - latTagFrom));
        int lngTagFrom = locationTag.IndexOf("<lng>") + "<lng>".Length;
        int lngTagTo = locationTag.LastIndexOf("</lng>");
        lng = Convert.ToDouble(locationTag.Substring(lngTagFrom, lngTagTo - lngTagFrom));
    }
}
