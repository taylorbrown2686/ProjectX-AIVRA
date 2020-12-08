using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
//This script checks for a desync between both AR and Unity framerates
public class FramerateCheck : PauseOnARError
{
    public ARSession arSessionFrames; //Gets AR framerate
    public float unityFPS, arFPS;
    public Text unityFPSText, arFPSText; //Text for FPS fields

    void Update() {
      unityFPS = (1.0f / Time.smoothDeltaTime);
      arFPS = Convert.ToSingle(arSessionFrames.frameRate);

      unityFPSText.text = unityFPS.ToString();
      arFPSText.text = arFPS.ToString();

      if (Mathf.Abs(unityFPS - arFPS) >= 3) {
        //pause game with 'FPS desynced';
        Debug.Log("Framerate Desync!!!");
      }
    }
}
