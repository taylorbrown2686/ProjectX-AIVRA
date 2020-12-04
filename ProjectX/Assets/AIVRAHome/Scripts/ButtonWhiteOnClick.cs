using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWhiteOnClick : MonoBehaviour
{
    public string buttonContainerName;
    [SerializeField] private GameObject selectedImage;

    private void TurnButtonOn()
    {
        selectedImage.SetActive(true);
    }

    public void TurnButtonOff()
    {
        selectedImage.SetActive(false);
    }

    public void TurnButtonsOff()
    {
        SelectedButtonMaster.Instance.TurnButtonsOff(buttonContainerName);
        TurnButtonOn();
    }
}
