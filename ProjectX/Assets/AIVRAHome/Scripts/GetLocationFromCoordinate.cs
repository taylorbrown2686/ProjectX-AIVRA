using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocationFromCoordinate : MonoBehaviour
{
      private string apiKey = "AIzaSyBaYR5jsJKXg12zxgJ-OtND6rMQsYikjWM";

      public IEnumerator GetLocationName(Vector2 coords) {
        WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + coords.x + "," + coords.y + "&key=" + apiKey);
        yield return www;
        this.gameObject.GetComponent<MarkerData>().Address = ExtractString(www.text, "formatted_address", "geometry", 5, 18);
        Destroy(this);
        //StartCoroutine(GetLocationImageDetails(this.gameObject.GetComponent<MarkerData>().Address));
      }

      /*public IEnumerator GetLocationImageDetails(string address) {
        WWW www = new WWW("https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + address + "&inputtype=textquery&key=" + apiKey);
        yield return www;
        Debug.Log(ExtractString(www.text, "place_id", "status", 5, 20));
        StartCoroutine(GetLocationImage(ExtractString(www.text, "place_id", "status", 5, 20)));
      }

      public IEnumerator GetLocationImage(string imageDetails) {
        WWW www = new WWW("https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + imageDetails + "&key=" + apiKey);
        yield return www;
        Debug.Log(www.error);
        this.gameObject.GetComponent<MarkerData>().LocationImage = www.texture;
      }*/

      private string ExtractString(string s, string firstStringIndex, string secondStringIndex, int firstStringTruncate, int secondStringTruncate) {
        int startIndex = s.IndexOf(firstStringIndex) + firstStringIndex.Length + firstStringTruncate;
        int endIndex = s.IndexOf(secondStringIndex, startIndex);
        return s.Substring(startIndex, endIndex - startIndex - secondStringTruncate);
      }
}
