using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to CoordinateContainer
public class PlayerCoordinates : MonoBehaviour
{
    private float playerLat, playerLng;
    private bool canRead = true;
    private static PlayerCoordinates _instance;
    public Text latText, lngText;

    public float PlayerLat {get => playerLat;}
    public float PlayerLng {get => playerLng;}
    public static PlayerCoordinates Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
    }

    void Update() {
      if (canRead) {
        StartCoroutine(ReadGPS(5f));
      }
    }

    private IEnumerator ReadGPS(float delay) {
      canRead = false;
      playerLat = Input.location.lastData.latitude;
      latText.text = "Lat: " + playerLat.ToString();
      playerLng = Input.location.lastData.longitude;
      lngText.text = "Lng: " + playerLng.ToString();
      yield return new WaitForSeconds(delay);
      canRead = true;
    }
}
