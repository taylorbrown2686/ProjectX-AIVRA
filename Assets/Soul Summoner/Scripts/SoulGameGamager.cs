using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulGameGamager : MonoBehaviour
{
    private static SoulGameGamager _instance;
    public CreateObject createObject;
    public Button iceButton, fireButton, shieldButton;
    public GameObject shield;
    private SoulGameGamager()
    {


    }

    public static SoulGameGamager Instance
    {
        get { return _instance; }
    }


    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
            Input.location.Start();
            createObject.gameObject.SetActive(true);
            iceButton.gameObject.SetActive(true);
            fireButton.gameObject.SetActive(true);
            shieldButton.gameObject.SetActive(true);
        }
    }

}
