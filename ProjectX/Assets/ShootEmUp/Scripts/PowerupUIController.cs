using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUIController : MonoBehaviour
{
    public Image powerupImage;
    public Sprite[] powerupSprites;
    private bool imageIsEmpty = true;

    public IEnumerator ChangeImage(int imageID) {
      if (imageIsEmpty) {
        imageIsEmpty = false;
        powerupImage.sprite = powerupSprites[imageID];
        yield return new WaitForSeconds(3f);
        imageIsEmpty = true;
      }
    }
}
