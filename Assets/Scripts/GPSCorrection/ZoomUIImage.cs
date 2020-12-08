using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This class allows zooming on a UI image automatically. Simply attach it to an object with the Image component on it
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class ZoomUIImage : MonoBehaviour
{
    public int iterations; //Public so we can modify in the inspector
    public float sizeToIncreaseBy; //Public for same reason

    void Start() {
      RectTransform transform = this.GetComponent<RectTransform>();
      StartCoroutine(ZoomImage(transform));
    }

    private IEnumerator ZoomImage(RectTransform rectTransform) {
      for (int i = 0; i < iterations; i++) {
        rectTransform.localScale += new Vector3(sizeToIncreaseBy, sizeToIncreaseBy, sizeToIncreaseBy);
        yield return new WaitForSeconds(0.1f);
      }
    }
}
