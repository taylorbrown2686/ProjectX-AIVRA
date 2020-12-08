using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextWithShadows : MonoBehaviour
{
    [SerializeField] private string textToSet;

    void Start() {
      SetText();
    }

    private void SetText() {
      textToSet = this.GetComponent<Text>().text;
      foreach (Text text in this.GetComponentsInChildren<Text>()) {
        text.text = textToSet;
      }
    }
}
