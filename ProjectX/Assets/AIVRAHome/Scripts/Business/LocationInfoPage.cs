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

    void Start()
    {
        //CHANGE BELOW LATER (FOR MULTIPLE BUSINESSES)
        businessDropdown.ClearOptions();
        businessDropdown.options.Add(new Dropdown.OptionData() { text = businessController.businessName });
        currentBusinessAddress.text = businessController.businessAddress;

        FocusedBusinessChanged();
    }

    public void UpdateLocation()
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
            //send changes to DB
        }
    }

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
        //businessDropdown.value = 1;
    }
}
