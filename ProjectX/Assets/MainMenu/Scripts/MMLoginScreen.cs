using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMLoginScreen : MonoBehaviour
{
    [SerializeField] private InputField username, password;
    [SerializeField] private Text errorText;
    private bool userIsVerified = false;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(0); //Main
      }
    }

    public void Login() {
      if (VerifyFields() == "No Errors") {
        if (userIsVerified) {
          SceneManager.LoadScene("AIVRAHome");
        } else {
          MMUIController.Instance.ChangeScreen(2); //Confirm Account
        }
      }
    }

    public void ForgotPassword() {
      if (username.text.Contains("@") && username.text.Contains(".")) {
        Email email = new Email();
        email.SendResetPasswordEmail(username.text, Random.Range(100000, 999999));
        MMUIController.Instance.ChangeScreen(3); //Forgot Pass
      } else {
        errorText.text = "Please enter a valid email address to send a confirmation email to.";
      }
    }

    private string VerifyFields() {
      //if () { TODO: Check db for correct info

      //}
      return "No Errors";
    }
}
