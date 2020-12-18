using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulProjectile : MonoBehaviour
{

    GameObject parent;
    PhotonView photonview;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        parent = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(parent.transform);
        transform.localScale = new Vector3(0.1f / parent.transform.localScale.x, 0.1f / parent.transform.localScale.y, 0.1f / parent.transform.localScale.z);
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
