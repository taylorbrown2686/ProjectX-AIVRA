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
    public GameObject shield,chargeBall;
    bool isShieldActive, button1, button2, button3, nbutton1, nbutton2, nbutton3,isCharging;
    public SoulAvatar sa;
    int level;
   


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

    public void fireButton3()
    {
        button3 = true;
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
            chargeBall.SetActive(isCharging);
            if (nbutton1 == true)
            {
                sa.Button1(level);
                nbutton1 = false;
            }
            if (nbutton2 == true)
            {
                sa.Button2(level);
                nbutton2 = false;
            }
            if (nbutton3 == true)
            {
                sa.Button3(level);
                nbutton3 = false;
            }

        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
            stream.SendNext(chargeBall.activeSelf);
            stream.SendNext(sa.level);
            stream.SendNext(shield.activeSelf);
            stream.SendNext(button1);
            stream.SendNext(button2);
            stream.SendNext(button3);
            button1 = false;
            button2 = false;
            button3 = false;

        }
        else if (stream.IsReading)
        {
            networkedposition = (Vector3)stream.ReceiveNext();
            networkedRotation = (Quaternion)stream.ReceiveNext();
            isCharging = (bool)stream.ReceiveNext();
            level = (int)stream.ReceiveNext();
            isShieldActive = (bool)stream.ReceiveNext();
            nbutton1 = (bool)stream.ReceiveNext();
            nbutton2 = (bool)stream.ReceiveNext();
            nbutton3 = (bool)stream.ReceiveNext();

        }
    }
}
