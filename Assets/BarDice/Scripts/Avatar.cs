using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Avatar : MonoBehaviour, IPunObservable
{

    GameObject gameZone, plane;
    PhotonView photonview;
    public TextMesh textMesh;
    GameObject parent;
    float FixeScale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        plane = gameZone.transform.GetChild(0).gameObject;
        transform.SetParent(plane.transform);
        parent = gameZone;
        textMesh.text = photonview.Owner.NickName;
        if (photonview.IsMine == true)
            GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine == true)
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1 * Time.deltaTime);
        else
            textMesh.gameObject.transform.LookAt(Camera.main.transform);

        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
