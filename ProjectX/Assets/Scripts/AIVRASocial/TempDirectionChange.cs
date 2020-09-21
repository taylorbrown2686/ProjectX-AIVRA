using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempDirectionChange : MonoBehaviour
{
    public bool moveNegative {get; private set;}
    public Sprite[] sprites;
    public Image btnImage;

    void Start() {
      moveNegative = false;
    }

    public void ChangeDirection() {
      if (moveNegative) {
        moveNegative = false;
        btnImage.sprite = sprites[0];
      } else {
        moveNegative = true;
        btnImage.sprite = sprites[1];
      }
    }
}
