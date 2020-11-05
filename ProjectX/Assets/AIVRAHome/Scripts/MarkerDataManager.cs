using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDataManager : MonoBehaviour
{
    private MarkerData[] markerData;
    private static MarkerDataManager _instance;

    public MarkerData[] MarkerData {get => markerData;}
    public static MarkerDataManager Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        markerData = FindObjectsOfType(typeof(MarkerData)) as MarkerData[];
        Debug.Log(markerData.Length);
        _instance = this;
      }
    }
}
