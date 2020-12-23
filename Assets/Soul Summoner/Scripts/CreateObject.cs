using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CreateObject : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject cubePrefab;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    //Rraycast Planes
                    if (arRaycastManager.Raycast(touch.position, arRaycastHits))
                    {
                        var pose = arRaycastHits[0].pose;
                        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
                            CreateCube(pose.position);
                        else
                            Destroy(this.gameObject);
                        return;
                    }

                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.tag == "cube")
                        {
                            DeleteCube(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void CreateCube(Vector3 position)
    {
        
        PhotonNetwork.Instantiate("Soul Summoner\\Monster", position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void DeleteCube(GameObject cubeObject)
    {
        Destroy(cubeObject);
    }
}
