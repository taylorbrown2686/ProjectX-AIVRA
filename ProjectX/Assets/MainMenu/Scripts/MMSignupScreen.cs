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

    void OnEnable()
    {
        MMUIController.Instance.storedUserFields.Clear();
        MMUIController.Instance.storedBusinessFields.Clear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MMUIController.Instance.ChangeScreen(0); //Main
        }
    }

    public void Continue()
    {
        if (VerifyFields() == "No Errors")
        {
            StartCoroutine(CheckDBForDuplicates());
        }
        else
        {
            errorText.text = VerifyFields();
        }
    }

    public void IsBusiness()
    {
        isBusiness = !isBusiness;
        businessName.gameObject.SetActive(isBusiness);
        businessAddress.gameObject.SetActive(isBusiness);
    }

    private string VerifyFields()
    {
        if (firstName.text == "" || lastName.text == "" || birthday.text == "" || email.text == "" || phoneNumber.text == "")
        {
            return "A required field was left blank.";
        }
        if (isBusiness && (businessName.text == "" || businessAddress.text == ""))
        {
            return "A required field was left blank.";
        }
        if (!Regex.IsMatch(firstName.text, @"^[a-zA-Z]+$"))
        {
            return "Your first name was in an invalid format.";
        }
        if (!Regex.IsMatch(lastName.text, @"^[a-zA-Z]+$"))
        {
            return "Your last name was in an invalid format.";
        }
        if (!Regex.IsMatch(birthday.text, @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"))
        {
            return "Your birthday was in an invalid format. Use MM/DD/YYYY.";
        }
        if (!email.text.Contains("@") || !email.text.Contains(".") || email.text.Contains(" "))
        {
            return "Your email was in an invalid format.";
        }
        if (!Regex.IsMatch(phoneNumber.text, @"^[0-9]+$"))
        {
            return "Your phone number was in an invalid format. Use XXX-XXX-XXXX.";
        }
        if (isBusiness)
        {
            int commaCount = 0;
            foreach (char c in businessAddress.text)
            {
                if (c.Equals(','))
                {
                    commaCount += 1;
                }
            }
            if (commaCount < 2)
            {
                return "Your business address was in an invalid format. Include city, state, and zip code, separated by a comma.";
            }
        }
        //if (emailExists || phoneNumberExists) { TODO: DB check

        //}
        return "No Errors";
    }

    private IEnumerator CheckDBForDuplicates()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        WWW www = new WWW("http://localhost:8080/AIVRA-PHP/checkEmailExists.php", form);
        yield return www;
        Debug.Log(www.text);
        if (www.text == "1")
        {
            errorText.text = "Your email is already in use.";
            yield break;
        }
        WWWForm form2 = new WWWForm();
        form2.AddField("phoneNumber", phoneNumber.text);
        WWW www2 = new WWW("http://localhost:8080/AIVRA-PHP/checkPhoneExists.php", form2);
        yield return www2;
        if (www2.text == "1")
        {
            errorText.text = "Your phone number is already in use.";
            yield break;
        }
        //Add fields and load next screen
        MMUIController.Instance.AddValueToStoredFields("fullName", firstName.text + " " + lastName.text, false);
        MMUIController.Instance.AddValueToStoredFields("birthdate", birthday.text, false);
        MMUIController.Instance.AddValueToStoredFields("email", email.text, false);
        MMUIController.Instance.AddValueToStoredFields("email", email.text, true);
        MMUIController.Instance.AddValueToStoredFields("phoneNumber", phoneNumber.text, false);
        if (isBusiness)
        {
            MMUIController.Instance.AddValueToStoredFields("businessName", businessName.text, true);
            MMUIController.Instance.AddValueToStoredFields("businessAddress", businessAddress.text, true);
            MMUIController.Instance.businessAddress = businessAddress.text;
        }
        MMUIController.Instance.email = email.text;
        if (isBusiness)
        {
            MMUIController.Instance.ChangeScreen(5);
        }
        else
        {
            MMUIController.Instance.ChangeScreen(6);
        }
    }
}
