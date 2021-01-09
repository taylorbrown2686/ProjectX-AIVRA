using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1LeiniesController : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (this.gameObject.name == "LeiniesShop")
        {
            Application.OpenURL("https://www.leinie.com/av?url=https://www.leinie.com/leinie-lodge");
        }
        else
        {
            Application.OpenURL("https://www.facebook.com/pages/Jacob-Leinenkugel-Brewing-Company/107910012571103");
        }
    }
}
