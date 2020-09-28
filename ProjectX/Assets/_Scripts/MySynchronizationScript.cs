using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class MySynchronizationScript : PunBehaviour, IPunObservable
{
    Rigidbody rb;
    PhotonView pw;

    Vector3 networkedPosition;
    Quaternion networkedRotation;
    public bool synchronizeVelocity = true;
    public bool synchronizeAngularVelocity = true;
    public bool isTeleportEnabled = true;
    public float teleportIfDistanceGreaterThan = 1.0f;

    private float distance;
    private float angle;

    private GameObject battleArenaGameobject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();

        networkedPosition = new Vector3();
        networkedRotation = new Quaternion();

        battleArenaGameobject = GameObject.Find("BattleArena");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkedPosition, distance * (1.0f / PhotonNetwork.sendRateOnSerialize));
            rb.rotation = Quaternion.RotateTowards(rb.rotation, networkedRotation, angle * (1.0f / PhotonNetwork.sendRateOnSerialize));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //then, photonView is mine and i am the one who controls the player
            //should send data to other players
            stream.SendNext(rb.position - battleArenaGameobject.transform.position);
            stream.SendNext(rb.rotation);

            if (synchronizeVelocity) {
                stream.SendNext(rb.velocity);
              
            }

            if (synchronizeAngularVelocity) {
                stream.SendNext(rb.angularVelocity);
            }

        }
        else {
            //called on player gameobject that exists in remote player game
            networkedPosition = (Vector3)stream.ReceiveNext() + battleArenaGameobject.transform.position;
            networkedRotation = (Quaternion)stream.ReceiveNext();

            if (isTeleportEnabled)
            {
                if (Vector3.Distance(rb.position, networkedPosition) > teleportIfDistanceGreaterThan)
                {
                    rb.position = networkedPosition;

                }
            }

            if (synchronizeVelocity || synchronizeAngularVelocity)
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));

                if (synchronizeVelocity)
                {
                    rb.velocity = (Vector3)stream.ReceiveNext();

                    networkedPosition += rb.velocity * lag;

                    distance = Vector3.Distance(rb.position, networkedPosition);
                }

                if (synchronizeAngularVelocity)
                {
                    rb.angularVelocity = (Vector3)stream.ReceiveNext();

                    networkedRotation = Quaternion.Euler(rb.angularVelocity * lag) * networkedRotation;

                    angle = Quaternion.Angle(rb.rotation, networkedRotation);
                }
            }
        }
    }

}
