using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUIController : MonoBehaviour
{
    public Image powerupImage;
    public Sprite[] powerupSprites;
    private bool imageIsEmpty = true;

    void Start() {
      powerupImage.enabled = false;
    }

    public IEnumerator ChangeImage(int imageID) {
      if (imageIsEmpty) {
        EnableOrDisable(true);
        imageIsEmpty = false;
        powerupImage.sprite = powerupSprites[imageID];
        yield return new WaitForSeconds(3f);
        imageIsEmpty = true;
        EnableOrDisable(false);
      }
    }

    private void EnableOrDisable(bool enable) {
      if (enable) {
        powerupImage.enabled = true;
      } else {
        powerupImage.enabled = false;
      }
    }
}
