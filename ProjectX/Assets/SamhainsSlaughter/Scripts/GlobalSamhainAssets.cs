using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSamhainAssets : MonoBehaviour
{
    public GameObject[] areas;
    public Sprite[] smilies;
    public GameObject playerUI, nowEnteringUI, deathUI;
    public AudioClip deathAmbience;

    private static GlobalSamhainAssets _instance;
    public static GlobalSamhainAssets Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
    }
}
