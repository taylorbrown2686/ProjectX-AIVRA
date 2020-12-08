using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMConfirmAccountScreen : MonoBehaviour
{
    [SerializeField] private InputField code;
    [SerializeField] private Text errorText;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(1); //Login
      }
    }

    public void Continue() {
      //check code in db
      SceneManager.LoadScene("AIVRAHome");
    }
}
