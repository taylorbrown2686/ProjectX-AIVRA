using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixArrow : MonoBehaviour
{
    GameObject gameZone;
    int minI = 0;
    float distance, min;
    // Start is called before the first frame update
    void Start()
    {
        gameZone = transform.parent.gameObject;
        min = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {

        
        
       
        
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
            return;
            for (int i = 0; i < 360; i++) { 
            gameZone.transform.rotation = Quaternion.Euler(0, i, 0);
            distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if (distance < min) { 
                min = distance;
                minI = i;
            }
        }

        min = float.MaxValue;

        gameZone.transform.rotation = Quaternion.Euler(0, minI, 0);
    }
}
