using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to NearbyEventBox
public class AIVRASays : MonoBehaviour
{

    private RectTransform transform;
    private Text aivraText;

    void Start() {
      transform = this.GetComponent<RectTransform>();
      aivraText = this.GetComponentInChildren<Text>();
    }

    public IEnumerator Say(string message) {
      while (transform.sizeDelta.x < 500f) {
        transform.sizeDelta += new Vector2(15f, 10f);
        yield return new WaitForSeconds(0.01f);
      }
      aivraText.text = message;
      yield return new WaitForSeconds(5f);
      aivraText.text = "";
      while (transform.sizeDelta.x > 0) {
        transform.sizeDelta -= new Vector2(15f, 10f);
        yield return new WaitForSeconds(0.01f);
      }
    }

}
