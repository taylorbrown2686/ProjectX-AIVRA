using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DiceSync : MonoBehaviour, IPunObservable
{
    Rigidbody rb;
    PhotonView photonview;

    Vector3 networkedposition;
    Quaternion networkedRotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonview = GetComponent<PhotonView>();

        networkedposition = new Vector3();
        networkedRotation = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
 //       Debug.Log(photonview.IsMine);
        if (photonview.IsMine == false)
        {

            transform.localPosition = new Vector3(networkedposition.x, networkedposition.y, networkedposition.z);
            transform.localRotation = networkedRotation;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
         //   stream.SendNext(rb.velocity);

        }
        else if (stream.IsReading)
        {
            networkedposition = (Vector3)stream.ReceiveNext();
            networkedRotation = (Quaternion)stream.ReceiveNext();
         //   float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
         //   rb.position += rb.velocity * lag;
        }
    }
}
