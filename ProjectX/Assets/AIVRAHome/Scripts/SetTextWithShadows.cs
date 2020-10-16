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
      this.GetComponent<Text>().text = textToSet;
      foreach (Text text in this.GetComponentsInChildren<Text>()) {
        text.text = textToSet;
      }
    }
}
