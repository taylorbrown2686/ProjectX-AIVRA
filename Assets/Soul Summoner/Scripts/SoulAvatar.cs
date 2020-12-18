using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SoulAvatar : MonoBehaviour, IPunObservable
{
    GameObject gameZone;
    PhotonView photonview;
    GameObject parent;
    float FixeScale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(gameZone.transform);
        parent = gameZone;

        if (photonview.IsMine == true)
            GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine == true) { 
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1 * Time.deltaTime);

            if (Input.GetButton("Fire1"))
            {
                Rigidbody bullet = PhotonNetwork.Instantiate("Soul Summoner\\Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(Camera.main.transform.forward * 100);

            }

        }
        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

}