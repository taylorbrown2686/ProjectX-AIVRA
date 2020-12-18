using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGameGamager : MonoBehaviour
{
    private static SoulGameGamager _instance;
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
        }
    }
}
