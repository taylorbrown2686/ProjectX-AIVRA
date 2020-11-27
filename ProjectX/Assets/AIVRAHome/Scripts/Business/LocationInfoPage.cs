using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoPage : MonoBehaviour
{
    [SerializeField] private GameObject updatingMarkerContainer, scaleMarkerSlider, updateLocationInfoWindow, updatingMarkerWindow;
    [SerializeField] private Text currentBusinessAddress, updateMarkerButtonText;
    [SerializeField] private Dropdown businessDropdown;
    private string currentBusinessName;
    [SerializeField] private InputField editBusinessName, editBusinessAddress;
    private bool updatingMarker = false;
    private bool updatingLocationInfo = false;
    [SerializeField] private BusinessController businessController;
    [SerializeField] private OnlineMaps map;

    void Start()
    {
        //CHANGE BELOW LATER (FOR MULTIPLE BUSINESSES)
        businessDropdown.ClearOptions();
        foreach (KeyValuePair<string, string> pair in businessController.businessNamesAndAddresses)
        {
            businessDropdown.options.Add(new Dropdown.OptionData() { text = pair.Key });
        }
        currentBusinessAddress.text = businessController.businessAddress;
        businessDropdown.value = 1;
    }

    /*public void UpdateLocation()
    {
        if (!updatingLocationInfo)
        {
            updatingLocationInfo = true;
            updateLocationInfoWindow.SetActive(true);
            editBusinessName.text = currentBusinessName;
            editBusinessAddress.text = currentBusinessAddress.text;
        }
        else
        {
            updatingLocationInfo = false;
            updateLocationInfoWindow.SetActive(false);
            if (businessController.businessName != editBusinessName.text)
            {
                if (businessController.businessAddress != editBusinessAddress.text)
                {
                    Debug.Log("UPDATE BOTH");
                } 
                else
                {
                    Debug.Log("UPDATE NAME");
                }
            }
            else
            {
                Debug.Log("UPDATE ADDY");
            }
        }
    }

    private IEnumerator SendLocationUpdatesToDB(string fieldType, string fieldUpdateValue, bool changeBoth, string fieldUpdateValue2)
    {
        WWWForm form = new WWWForm();
        form.AddField("fieldType", fieldType);
        form.AddField("fieldToUpdate", fieldUpdateValue);
        if (changeBoth)
        {
            form.AddField("fieldToUpdate2", fieldUpdateValue2);
        }
        form.AddField("businessName", businessController.businessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/updateBusinessNameOrAddress.php", form);
        yield return www;
        break;
    }*/ //REQUIRES SPECIAL CONTACT, TOO MANY DB FIELDS TO UPDATE

    public void UpdateMarker()
    {
        if (!updatingMarker)
        {
            updatingMarker = true;
            updatingMarkerWindow.SetActive(true);
            updateMarkerButtonText.text = "Done";
            updatingMarkerContainer.SetActive(true);
            scaleMarkerSlider.SetActive(true);
        }
        else
        {
            updatingMarker = false;
            updatingMarkerWindow.SetActive(false);
            updateMarkerButtonText.text = "Update Marker";
            updatingMarkerContainer.SetActive(false);
            scaleMarkerSlider.SetActive(false);
        }
    }

    public void FocusedBusinessChanged()
    {
        List<Dropdown.OptionData> menuOptions = businessDropdown.options;
        currentBusinessName = menuOptions[businessDropdown.value].text;
        businessController.businessName = currentBusinessName;
        foreach (KeyValuePair<string, string> pair in businessController.businessNamesAndAddresses)
        {
            if (pair.Key == currentBusinessName)
            {
                currentBusinessAddress.text = pair.Value;
                businessController.businessAddress = pair.Value;
                foreach (KeyValuePair<string, string> pair2 in businessController.businessCoordinates)
                {
                    if (pair2.Key == currentBusinessName)
                    {
                        string[] splitString = pair2.Value.Split(',');
                        map.SetPosition(Convert.ToDouble(splitString[1]), Convert.ToDouble(splitString[0]));
                    }
                }
                break;
            }
        }
    }
}
