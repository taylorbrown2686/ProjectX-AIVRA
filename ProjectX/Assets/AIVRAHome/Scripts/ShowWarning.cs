using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour
{
    [SerializeField] private GameObject warningScreen;
    private bool dontShowAgain = false;
    private void Start()
    {
        if (PlayerPrefs.GetInt("showwarning") == 1)
        {
            warningScreen.SetActive(true);
        }
    }

    public void ToggleDontShowAgain()
    {
        dontShowAgain = !dontShowAgain;
    }

    public void Accept()
    {
        if (dontShowAgain)
        {
            PlayerPrefs.SetInt("showwarning", 0);
            Destroy(warningScreen);
        }
        else
        {
            PlayerPrefs.SetInt("showwarning", 1);
            Destroy(warningScreen);
        }
    }
}
