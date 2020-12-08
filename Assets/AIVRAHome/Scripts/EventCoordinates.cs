using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attached to ARLocationRoot
public class EventCoordinates : MonoBehaviour
{
    public Dictionary<Vector2, int> coordsWithUID;
    private static EventCoordinates _instance;

    public static EventCoordinates Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        coordsWithUID = new Dictionary<Vector2, int>();
        _instance = this;
      }
    }
}
