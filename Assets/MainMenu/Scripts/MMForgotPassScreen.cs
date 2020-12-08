using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMForgotPassScreen : MonoBehaviour
{
    [SerializeField] private InputField password, confPassword, code;
    [SerializeField] private Text errorText;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(1); //Login
      }
    }

    public void Continue() {
      if (VerifyFields() == "No Errors") {
        MMUIController.Instance.ChangeScreen(1); //Login
      } else {
        errorText.text = VerifyFields();
      }
      //check code
    }

    private string VerifyFields() {
      if (password.text != confPassword.text) {
        return "Your passwords did not match";
      }
      return "No Errors";
    }
}
