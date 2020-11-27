using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//PAGE INDEXES: Main-0, Login-1, ConfirmAccount-2, ForgotPassword-3, Signup-4, BusinessMap-5, Credentials-6, VerifyingCreation-7
public class MMUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    public Dictionary<string, string> storedUserFields = new Dictionary<string, string>();
    public Dictionary<string, string> storedBusinessFields = new Dictionary<string, string>();
    public string businessAddress; //needed for geocoding with google
    public string email; //needed for sending confirmation email
    public string usernameOrEmail; //needed for verification with code
    public bool stayLoggedIn = false;

    private static MMUIController instance = null;

    public static MMUIController Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DisableAllScreens();
        screens[0].SetActive(true);
    }

    public void ChangeScreen(int index)
    {
        DisableAllScreens();
        screens[index].SetActive(true);
    }

    private void DisableAllScreens()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }

    public void AddValueToStoredFields(string fieldKey, string fieldValue, bool isBusiness)
    {
        string formattedValue = null;
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        int i = 0;
        switch (fieldKey)
        {
            case "birthdate":
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
        if (formattedValue == null)
        {
            if (isBusiness)
            {
                storedBusinessFields.Add(fieldKey, fieldValue);
            } 
            else
            {
                storedUserFields.Add(fieldKey, fieldValue);
            }
        }
        else
        {
            if (isBusiness)
            {
                storedBusinessFields.Add(fieldKey, fieldValue);
            }
            else
            {
                storedUserFields.Add(fieldKey, fieldValue);
            }
        }
    }

    public IEnumerator SendDataToDatabase()
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> pair in storedUserFields)
        {
            form.AddField(pair.Key, pair.Value);
        }
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadUserAccount.php", form);
        yield return www;

        if (storedBusinessFields.Count != 0)
        {
            WWWForm form2 = new WWWForm();
            foreach (KeyValuePair<string, string> pair in storedBusinessFields)
            {
                form2.AddField(pair.Key, pair.Value);
            }
            WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/uploadBusinessAccount.php", form2);
            yield return www2;
        }
    }
}
