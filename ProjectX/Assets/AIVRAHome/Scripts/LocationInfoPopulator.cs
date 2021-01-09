using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoPopulator : MonoBehaviour
{
    public Text locationName, locationAddress, eventName, eventDesc, eventSubDesc;
    public RawImage locationImage;

    public void PopulateLocationInfo(string markerUID) {
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        if (data.UID == Convert.ToInt32(markerUID)) {
          locationName.text = data.businessName; //THIS WILL CHANGE WHEN FIELD BECOMES PRIVATE
          locationAddress.text = data.Address;
          eventName.text = data.HostedEvents[0].EventType + ": " + data.HostedEvents[0].EventTitle;
          eventDesc.text = data.HostedEvents[0].EventDesc;
          eventSubDesc.text = data.HostedEvents[0].EventSubDesc;
          locationImage.texture = data.LocationImage;
        }
      }
    }

    public string GetLocationAddress(string markerUID) {
      foreach (MarkerData data in MarkerDataManager.Instance.MarkerData) {
        if (data.UID == Convert.ToInt32(markerUID)) {
          return data.Address;
        }
      }
      return "";
    }
}
