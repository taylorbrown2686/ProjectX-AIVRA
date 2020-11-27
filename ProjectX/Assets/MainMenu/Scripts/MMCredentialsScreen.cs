﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MMCredentialsScreen : MonoBehaviour
{
    [SerializeField] private InputField username, password, confPassword;
    [SerializeField] private Text errorText;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(4); //Signup
      }
    }

    public void Continue() {
      if (VerifyFields() == "No Errors") {
        StartCoroutine(CheckDBForDuplicates());
      } else {
        errorText.text = VerifyFields();
      }
    }

    private string VerifyFields() {
      if (username.text == "" || password.text == "" || confPassword.text == "") {
        return "A required field was left blank.";
      }
      if (!Regex.IsMatch(username.text, @"^[a-zA-Z0-9]*$")) {
        return "Your username must be alphanumeric, no symbols are allowed.";
      }
      if (password.text != confPassword.text) {
        return "Your passwords do not match.";
      }
      var hasNumber = new Regex(@"[0-9]+");
      var hasUpperChar = new Regex(@"[A-Z]+");
      var hasMinimum8Chars = new Regex(@".{8,}");
      if (!hasNumber.IsMatch(password.text) || !hasUpperChar.IsMatch(password.text) || !hasMinimum8Chars.IsMatch(password.text)) {
        return "Your password must have a capital, symbol, number, and be 8+ characters.";
      }
      //if (usernameExists) { TODO: DB check

      //}
      return "No Errors";
    }

    private IEnumerator CheckDBForDuplicates()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkUsernameExists.php", form);
        yield return www;
        if (www.text == "1")
        {
            errorText.text = "Your username is already in use.";
            yield break;
        }
        //Add fields and load next screen
        MMUIController.Instance.AddValueToStoredFields("username", username.text, false);
        MMUIController.Instance.AddValueToStoredFields("password", password.text, false);
        MMUIController.Instance.ChangeScreen(7); //Verifying
    }
}
