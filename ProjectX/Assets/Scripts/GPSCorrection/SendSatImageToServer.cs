using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
//This script sends the satellite image on the map to Azure for Tensorflow Neural Network processing and receives the result
public class SendSatImageToServer : MonoBehaviour
{
    private OnlineMaps map;
    private string[] coordinates;
    private bool portalsSpawned = false;

    public bool PortalsSpawned {get {return portalsSpawned;} set {portalsSpawned = value;}}

    void Start() {
      map = this.gameObject.GetComponent<OnlineMaps>(); //Reference this for zoom level
    }

    public IEnumerator SendImageToServer() {
      yield return new WaitForSeconds(1f); //TODO: Think of a better way to send the texture. We should be waiting for the
                                           //map tile to load instead of waiting a set time, but I need to figure out how
                                           //to do that.
      Texture2D mainTex = this.GetComponent<Renderer>().sharedMaterial.mainTexture as Texture2D;
      byte[] bytes = mainTex.EncodeToJPG();

      WWWForm form = new WWWForm();
      form.AddField("latitude", map.latitude.ToString());
      form.AddField("longitude", map.longitude.ToString());
      form.AddBinaryData("content", bytes, "ggg.png", "image/jpg");
      using (UnityWebRequest www = UnityWebRequest.Post("http://255446f6fb1e.ngrok.io/files/ggg.jpg", form)) {

          yield return www.SendWebRequest();

          if (www.isNetworkError || www.isHttpError)
          {
              Debug.Log(www.error);
          }
          else
          {
            string[] split = www.downloadHandler.text.Split('#');
            this.gameObject.GetComponent<SpawnPortalMarkers>().SpawnPortalsAtCoordinates(split);
            portalsSpawned = true;
          }
      }
    }
}
