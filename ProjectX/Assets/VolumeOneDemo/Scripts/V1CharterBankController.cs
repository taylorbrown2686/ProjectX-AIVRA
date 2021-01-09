using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1CharterBankController : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (this.gameObject.name == "GoToSite")
        {
            Application.OpenURL("www.charterbank.bank");
        }
        else
        {
            Application.OpenURL("tel://7158324254");
        }
    }
}
