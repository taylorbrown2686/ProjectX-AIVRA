using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//This script sends the satellite image on the map to Azure for Tensorflow Neural Network processing and receives the result
public class SendSatImageToServer : MonoBehaviour
{
    private OnlineMaps map;

    void Start() {
      map = this.gameObject.GetComponent<OnlineMaps>();
    }

    public IEnumerator SendImageToServer() {
      yield return new WaitForSeconds(1f); //TODO: Think of a better way to send the texture. We should be waiting for the
                                           //map tile to load instead of waiting a set time, but I need to figure out how
                                           //to do that.
      Texture2D mainTex = this.GetComponent<Renderer>().sharedMaterial.mainTexture as Texture2D;
      byte[] bytes = mainTex.EncodeToPNG();
      File.WriteAllBytes("C:/temp/" + map.zoom + ".png", bytes);
    }
}
