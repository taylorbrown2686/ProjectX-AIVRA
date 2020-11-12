using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowEnteringController : MonoBehaviour
{
    public Image nowEnteringScreen, nowEnteringText, areaImage, enemyImageOne, enemyImageTwo;
    public Text areaText, enemyText;
    public Sprite[] areaImages, enemyImages;
    private static NowEnteringController _instance;

    public static NowEnteringController Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
    }
}
