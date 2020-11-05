using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
//PAGE INDEXES: Main-0, Login-1, ConfirmAccount-2, ForgotPassword-3, Signup-4, BusinessMap-5, Credentials-6, VerifyingCreation-7
public class MMUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    public Dictionary<string, string> storedFields = new Dictionary<string, string>();
    public string businessAddress; //needed for geocoding with google
    public string email; //needed for sending confirmation email

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
      string formattedValue = null;
      TextInfo ti = new CultureInfo("en-US", false).TextInfo;
      int i = 0;
      switch (fieldKey) {
        case "birthday":
          formattedValue = DateTime.Parse(fieldValue).ToString("MM/dd/yyyy");
        break;

        case "phoneNumber":
          formattedValue = fieldValue.Replace(@"/[^0-9]/g", "");
        break;

        case "businessName":
          formattedValue = ti.ToTitleCase(fieldValue);
        break;

        case "businessAddress":
          formattedValue = ti.ToTitleCase(fieldValue);
        break;
      }
      if (formattedValue == null) {
        storedFields.Add(fieldKey, fieldValue);
      } else {
        storedFields.Add(fieldKey, formattedValue);
      }
    }

    public void DebugAllDictValues() {
      foreach (KeyValuePair<string, string> pair in storedFields) {
        Debug.Log(pair.Key + ", " + pair.Value);
      }
    }
}
