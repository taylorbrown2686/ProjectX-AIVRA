using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager ar_raycast;
    [SerializeField]
    private Transform playerPrefab;

    private Transform player;
    private static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }
  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Debug.Log(mousePos);
           // bool OverUI = mousePos.IsPointOverUIObject();
            bool OverUI = false;

            if (!OverUI)
            {
                Debug.Log("Not on UI");
                if (ar_raycast.Raycast(mousePos, raycast_Hits, TrackableType.PlaneWithinPolygon))
                {
                    //   StateText.text = "Hit Detected " + mousePos;
                    Pose hitPose = raycast_Hits[0].pose;
                    Vector3 positionToBePlaced = hitPose.position;
                    
                    player.position = positionToBePlaced;
                    Rigidbody rb = player.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameCallback() {
        if (PhotonNetwork.connected) {

        }
    }
    
}
