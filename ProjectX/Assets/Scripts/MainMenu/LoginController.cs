using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public InputField username, password; //setting up inputfield variables

    void OnEnable() { //when script is enabled, reset all text fields
      username.text = "";
      password.text = "";
    }

    public void LoginOnClick() { //we cannot call coroutines from buttons
      StartCoroutine(LoginTEMP()); //call the coroutine in an onclick method instead
    }

    private IEnumerator Login() {
      //TODO: Login with DB here
      yield return null;
    }

    private IEnumerator LoginTEMP() { //TEMPORARY CHECK
      if (username.text.ToLower() == "admin" && password.text.ToLower() == "admin") { //check lowercase fields for match to 'admin'
        SceneManager.LoadScene("GPSCorrectionTesting"); //load scene if they are right
        yield return null;
      } else {
        //error log your password was wrong!
        Debug.Log("Your username or password was incorrect!"); //break if not
        yield return null;
      }
    }
}
