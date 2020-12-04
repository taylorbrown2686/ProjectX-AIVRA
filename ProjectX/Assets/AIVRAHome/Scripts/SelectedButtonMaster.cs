using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedButtonMaster : MonoBehaviour
{
    [SerializeField] private ButtonWhiteOnClick[] selectedButtonImages;

    private static SelectedButtonMaster instance = null;

    public static SelectedButtonMaster Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TurnButtonsOff(string buttonContainerName)
    {
        foreach (ButtonWhiteOnClick bwoc in selectedButtonImages)
        {
            if (bwoc.buttonContainerName == buttonContainerName)
            {
                bwoc.TurnButtonOff();
            }
        }
    }
}
