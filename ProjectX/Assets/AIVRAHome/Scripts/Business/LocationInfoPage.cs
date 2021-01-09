using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoPage : MonoBehaviour
{
    [SerializeField] private GameObject yourGalleryWindow;
    [SerializeField] private Text currentBusinessAddress;
    [SerializeField] private Dropdown businessDropdown;
    private string currentBusinessName;
    [SerializeField] private BusinessController businessController;
    [SerializeField] private OnlineMaps map;

    void Start()
    {
        businessDropdown.ClearOptions();
        foreach (KeyValuePair<string, string> pair in businessController.businessNamesAndAddresses)
        {
            businessDropdown.options.Add(new Dropdown.OptionData() { text = pair.Key });
        }
        currentBusinessAddress.text = businessController.businessAddress;
        businessDropdown.value = 1;
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

    public void GoToGallery()
    {
        yourGalleryWindow.SetActive(true);
        yourGalleryWindow.GetComponent<YourGalleryController>().currentBusinessName = currentBusinessName;
        this.gameObject.SetActive(false);
    }
}
