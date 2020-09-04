using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupController : MonoBehaviour
{
  public InputField email, username, password, confirmPass; //setting up inputfield variables

  void OnEnable() { //when script is enabled, reset all text fields
    email.text = "";
    username.text = "";
    password.text = "";
    confirmPass.text = "";
  }

  public void LoginOnClick() { //we cannot call coroutines from buttons
    StartCoroutine(Signup()); //call the coroutine in an onclick method instead
  }

  private IEnumerator Signup() {
    //TODO: Signup with DB here
    yield return null;
  }
}
