using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMVerifyingScreen : MonoBehaviour
{
    [SerializeField] private Text verifyingText;

    IEnumerator Start() {
      verifyingText.text = "Verifying your information";
      for (int i = 0; i < 3; i++) {
        verifyingText.text += ".";
        yield return new WaitForSeconds(0.5f);
      }
      yield return new WaitForSeconds(1.5f);
      verifyingText.text += "\n" + "Saving data in database";
      for (int i = 0; i < 3; i++) {
        verifyingText.text += ".";
        yield return new WaitForSeconds(0.5f);
      }
      yield return new WaitForSeconds(2f);
      verifyingText.text += "\n" + "Your account was created successfully!";
      yield return new WaitForSeconds(2f);
      MMUIController.Instance.ChangeScreen(1); //Login
    }
}
