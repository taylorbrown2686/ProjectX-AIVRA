using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//This script checks the plane manager to detect when planes are lost
public class TrackingLostCheck : PauseOnARError
{
    public ARSession arSession;

    void Update() {
        TrackingState trackingState = arSession.subsystem.trackingState;
        Debug.Log(trackingState);
        NotTrackingReason notTracking = arSession.subsystem.notTrackingReason;
        Debug.Log(notTracking);
    }
}
