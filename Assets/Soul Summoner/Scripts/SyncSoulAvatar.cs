using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncSoulAvatar : MonoBehaviour, IPunObservable
{
    Rigidbody rb;
    PhotonView photonview;

    Vector3 networkedposition;
    Quaternion networkedRotation;
    public GameObject shield;
    bool isShieldActive, button1, button2, nbutton1, nbutton2;
    public SoulAvatar sa;
   


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        photonview = GetComponent<PhotonView>();

        networkedposition = new Vector3();
        networkedRotation = new Quaternion();
    }

    public void fireButton1()
    {
        button1 = true;
    }

    public void fireButton2()
    {
        button2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //       Debug.Log(photonview.IsMine);
        if (photonview.IsMine == false)
        {

            transform.localPosition = new Vector3(networkedposition.x, networkedposition.y, networkedposition.z);
            transform.localRotation = networkedRotation;
            shield.SetActive(isShieldActive);
            if (nbutton1 == true)
            {
                sa.Button1();
                nbutton1 = false;
            }
            if (nbutton2 == true)
            {
                sa.Button2();
                nbutton2 = false;
            }

        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
            stream.SendNext(shield.activeSelf);
            stream.SendNext(button1);
            stream.SendNext(button2);
            button1 = false;
            button2 = false;
            //   stream.SendNext(rb.velocity);

        }
        else if (stream.IsReading)
        {
            networkedposition = (Vector3)stream.ReceiveNext();
            networkedRotation = (Quaternion)stream.ReceiveNext();
            isShieldActive = (bool)stream.ReceiveNext();
            nbutton1 = (bool)stream.ReceiveNext();
            nbutton2 = (bool)stream.ReceiveNext();

            //   float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            //   rb.position += rb.velocity * lag;
        }
    }
}
