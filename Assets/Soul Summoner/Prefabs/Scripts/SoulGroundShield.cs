using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.ARFoundation;

public class SoulGroundShield : MonoBehaviour
{
    PhotonView photonview;
    public Vector3 target;
    public ARRaycastManager arRaycastManager;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        arRaycastManager = SoulGameManager.Instance.arRaycastManager;


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if (photonview.IsMine == true)
        {
            target = Camera.main.transform.position;
            transform.LookAt(target, Vector3.up);
            Quaternion targetQ = Quaternion.Euler(0, 90 + transform.rotation.eulerAngles.y, 0);
            transform.rotation = targetQ;

          /*  if (arRaycastManager.Raycast(new Vector2(Screen.currentResolution.width, Screen.currentResolution.height), arRaycastHits))
            {
                var pose = arRaycastHits[0].pose;

                transform.position = pose.position;

            }*/
        }
    }

}

    

