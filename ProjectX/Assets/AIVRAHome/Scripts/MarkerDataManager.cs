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
      markerData = FindObjectsOfType(typeof(MarkerData)) as MarkerData[];

      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
    }
}
