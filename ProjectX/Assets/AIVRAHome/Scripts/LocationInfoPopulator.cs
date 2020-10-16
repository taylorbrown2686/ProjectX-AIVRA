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
          eventName.text = data.HostedEvent.EventType + ": " + data.HostedEvent.EventTitle;
          eventDesc.text = data.HostedEvent.EventDesc;
          eventSubDesc.text = data.HostedEvent.EventSubDesc;
          locationImage.texture = data.LocationImage;
        }
      }
    }
}
