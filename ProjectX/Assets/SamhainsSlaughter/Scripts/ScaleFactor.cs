using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFactor : MonoBehaviour
{
    public float scaleFactor;
    private static ScaleFactor _instance;

    public static ScaleFactor Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }

      scaleFactor = this.transform.localScale.x;
    }
}
