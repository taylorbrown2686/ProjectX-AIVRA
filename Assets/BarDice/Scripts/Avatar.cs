using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Avatar : MonoBehaviour, IPunObservable
{

    GameObject gameZone, plane;
    PhotonView photonview;
    public TextMesh textMesh;

   

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        plane = gameZone.transform.GetChild(0).gameObject;
        transform.SetParent(plane.transform);
        textMesh.text = photonview.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine == true)
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position - new Vector3(0,0.5f,0), 1 * Time.deltaTime);
        else
            textMesh.gameObject.transform.LookAt(Camera.main.transform);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
