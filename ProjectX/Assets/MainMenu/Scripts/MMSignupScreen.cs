using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MMSignupScreen : MonoBehaviour
{
    [SerializeField] private InputField firstName, lastName, birthday, email, phoneNumber, businessName, businessAddress;
    [SerializeField] private Text errorText;
    private bool isBusiness = false;

    void OnEnable() {
      MMUIController.Instance.storedFields.Clear();
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        MMUIController.Instance.ChangeScreen(0); //Main
      }
    }

    public void Continue() {
      if (VerifyFields() == "No Errors") {
        MMUIController.Instance.AddValueToStoredFields("firstName", firstName.text);
        MMUIController.Instance.AddValueToStoredFields("lastName", lastName.text);
        MMUIController.Instance.AddValueToStoredFields("birthday", birthday.text);
        MMUIController.Instance.AddValueToStoredFields("email", email.text);
        MMUIController.Instance.AddValueToStoredFields("phoneNumber", phoneNumber.text);
        MMUIController.Instance.AddValueToStoredFields("businessName", businessName.text);
        MMUIController.Instance.AddValueToStoredFields("businessAddress", businessAddress.text);
        MMUIController.Instance.businessAddress = businessAddress.text;
        MMUIController.Instance.email = email.text;
        if (isBusiness) {
          MMUIController.Instance.ChangeScreen(5);
        } else {
          MMUIController.Instance.ChangeScreen(6);
        }
      } else {
        errorText.text = VerifyFields();
      }
    }

    public void IsBusiness() {
      isBusiness = !isBusiness;
      businessName.gameObject.SetActive(isBusiness);
      businessAddress.gameObject.SetActive(isBusiness);
    }

    private string VerifyFields() {
      if (firstName.text == "" || lastName.text == "" || birthday.text == "" || email.text == "" || phoneNumber.text == "") {
        return "A required field was left blank.";
      }
      if (isBusiness && (businessName.text == "" || businessAddress.text == "")) {
        return "A required field was left blank.";
      }
      if (!Regex.IsMatch(firstName.text, @"^[a-zA-Z]+$")) {
        return "Your first name was in an invalid format.";
      }
      if (!Regex.IsMatch(lastName.text, @"^[a-zA-Z]+$")) {
        return "Your last name was in an invalid format.";
      }
      if (!Regex.IsMatch(birthday.text, @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$")) {
        return "Your birthday was in an invalid format. Use MM/DD/YYYY.";
      }
      if (!email.text.Contains("@") || !email.text.Contains(".") || email.text.Contains(" ")) {
        return "Your email was in an invalid format.";
      }
      if (!Regex.IsMatch(phoneNumber.text, @"^[0-9]+$")) {
        return "Your phone number was in an invalid format. Use XXX-XXX-XXXX.";
      }
      if (isBusiness) {
        int commaCount = 0;
        foreach (char c in businessAddress.text) {
          if (c.Equals(',')) {
            commaCount += 1;
          }
        }
        if (commaCount < 2) {
          return "Your business address was in an invalid format. Include city, state, and zip code, separated by a comma.";
        }
      }
      //if (emailExists || phoneNumberExists) { TODO: DB check

      //}
      return "No Errors";
    }
}
