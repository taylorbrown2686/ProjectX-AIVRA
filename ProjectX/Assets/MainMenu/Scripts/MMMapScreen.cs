using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMMapScreen : MonoBehaviour
{
    [SerializeField] private GameObject scaleMarkerSlider, firstStepText, secondStepText;

    void OnEnable() {
      scaleMarkerSlider.SetActive(false);
      firstStepText.SetActive(true);
      secondStepText.SetActive(false);
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(4); //Signup
      }
    }

    public void Continue() {
      if (!scaleMarkerSlider.activeSelf) {
        scaleMarkerSlider.SetActive(true);
        firstStepText.SetActive(false);
        secondStepText.SetActive(true);
      } else {
        MMUIController.Instance.ChangeScreen(6);
      }
    }
}
