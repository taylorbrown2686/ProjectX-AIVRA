using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a custom location provider for AR + GPS. It allows us to set a position manually
namespace ARLocation {

  public class AIVRACustomProvider : AbstractLocationProvider
  {
      //This provider should start with no coordinate being reported. Once a GPS position is touched, a public method
      //to set the position to the one touched will be called, and it will set the GPS position to that
      //Sensor data should be ignored until the GPS is set. After that, accel/gyro will handle the rest.
      public override string Name => "AIVRACustomProvider";

      public override bool IsCompassEnabled => true;

      public Location mockLocation = new Location();

      private void Start() {
        mockLocation.Latitude = 0;
        mockLocation.Longitude = 0;
        mockLocation.Altitude = 0;
      }

      protected override HeadingReading? ReadHeading()
      {
          var mainCamera = ARLocationManager.Instance.MainCamera;

          var transform = mainCamera.transform;

          var localEulerAngles = transform.localEulerAngles;
          return new HeadingReading
          {
              heading = localEulerAngles.y,
              magneticHeading = localEulerAngles.y,
              accuracy = 0,
              isMagneticHeadingAvailable = true,
              timestamp = (long)(Time.time * 1000)
          };
      }

      protected override LocationReading? ReadLocation()
      {
          return new LocationReading
          {
              latitude = mockLocation.Latitude,
              longitude = mockLocation.Longitude,
              altitude = mockLocation.Altitude,
              accuracy = 0.0,
              floor = -1,
              timestamp = (long)(Time.time * 1000)
          };
      }

      private bool requested = true;

      protected override void RequestLocationAndCompassUpdates()
      {
          requested = true;
      }

      protected override void UpdateLocationRequestStatus()
      {
          if (requested)
          {
              Status = LocationProviderStatus.Initializing;
              requested = false;
          }

          if (Status == LocationProviderStatus.Initializing)
          {
              Status = LocationProviderStatus.Started;
          }
      }

      public void SetSpoofedLocation(float lat, float lng) {
        mockLocation.Latitude = lat;
        mockLocation.Longitude = lng;
        mockLocation.Altitude = 0;
      }
  }

}
