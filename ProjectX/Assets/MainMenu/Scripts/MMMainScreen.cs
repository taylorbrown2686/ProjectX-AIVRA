using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMMainScreen : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("stayloggedin") == 1 && CrossSceneVariables.Instance.email == "")
        {
            CrossSceneVariables.Instance.email = PlayerPrefs.GetString("email");
            SceneManager.LoadScene("AIVRAHome");
        }
    }

    public void Login() {
      MMUIController.Instance.ChangeScreen(1); //Login
    }

    public void Signup() {
      MMUIController.Instance.ChangeScreen(4); //Signup
    }
}
