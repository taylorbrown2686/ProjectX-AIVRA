using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIVRASays : MonoBehaviour
{

    [SerializeField] private GameObject businessButton, inLocationContainer, notInLocationContainer;
    [SerializeField] private Text businessButtonText, helloText, youAreAtText;

    public IEnumerator EnteringLocation(string businessName)
    {
        //play animation
        yield return null;
        OpenMenu();
        businessButton.SetActive(true);
        businessButtonText.text = businessName;
        youAreAtText.text = "You are at: " + businessName;
    }

    public IEnumerator LeavingLocation()
    {
        //play animation
        yield return null;
        CloseMenu();
        businessButton.SetActive(false);
        businessButtonText.text = "Home";
        youAreAtText.text = "";
    }

    public void OpenMenu()
    {
        inLocationContainer.SetActive(true);
    }

    public void CloseMenu()
    {
        inLocationContainer.SetActive(false);
    }
}
