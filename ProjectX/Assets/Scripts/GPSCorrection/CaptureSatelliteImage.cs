using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSatelliteImage : MonoBehaviour
{
    private int counter = 0;

    void Update() {
      if (Input.GetKeyDown(KeyCode.T)) {
        Texture2D tex = (Texture2D)this.GetComponent<Renderer>().material.mainTexture;
        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(@"C:/temp/map" + counter + ".png", bytes);
        counter++;
      }
    }
}
