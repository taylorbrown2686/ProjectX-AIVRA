using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
//PAGE INDEXES: Main-0, Login-1, ConfirmAccount-2, ForgotPassword-3, Signup-4, BusinessMap-5, Credentials-6, VerifyingCreation-7
public class MMUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    protected Dictionary<string, string> storedFields = new Dictionary<string, string>();

    private static MMUIController instance = null;

    public static MMUIController Instance {get => instance;}

    void Start() {
      if (instance == null) {
        instance = this;
      }
      DisableAllScreens();
      screens[0].SetActive(true);
    }

    public void ChangeScreen(int index) {
      DisableAllScreens();
      screens[index].SetActive(true);
    }

    private void DisableAllScreens() {
        foreach (GameObject screen in screens) {
          screen.SetActive(false);
        }
    }

    public void AddValueToStoredFields(string fieldKey, string fieldValue) {
      int i = 0;
      switch (fieldKey) {

        case "FirstName":
          foreach (char c in fieldValue) {
            if (i == 0) {
              Char.ToUpper(c);
            } else {
              Char.ToLower(c);
            }
            i++;
          }
          //storedFields.Add();
        break;

        case "LastName":
          foreach (char c in fieldValue) {
            if (i == 0) {
              Char.ToUpper(c);
            } else {
              Char.ToLower(c);
            }
            i++;
          }
          //storedFields.Add();
        break;

        case "Birthday":
          fieldValue = DateTime.Parse(fieldValue).ToString("MM/dd/yyyy");
          //storedFields.Add();
        break;

        case "Email":
          //storedFields.Add();
        break;

        case "PhoneNumber":
          //storedFields.Add();
        break;

        case "BusinessName":
          //storedFields.Add();
        break;

        case "BusinessAddress":
          //storedFields.Add();
        break;

        case "Username":
          //storedFields.Add();
        break;

        case "Password":
          //storedFields.Add();
        break;

      }
    }
}
