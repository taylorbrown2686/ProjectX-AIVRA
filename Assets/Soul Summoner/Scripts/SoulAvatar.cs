using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class SoulAvatar : MonoBehaviour, IPunObservable
{
    GameObject gameZone;
    PhotonView photonview;
    GameObject parent;
    float FixeScale = 0.3f;
    bool reloading = false;
    string type = "ice";

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(gameZone.transform);
        parent = gameZone;

        SoulGameGamager.Instance.fireButton.onClick.AddListener(delegate { Changetype("fire"); });
        SoulGameGamager.Instance.iceButton.onClick.AddListener(delegate { Changetype("ice"); });
        //SoulGameGamager.Instance.shieldButton.onClick.AddListener(delegate { CreateShield(); });

        EventTrigger trigger = SoulGameGamager.Instance.shieldButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { RemoveShield(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { CreateShield(); });
        trigger.triggers.Add(entry);

        if (photonview.IsMine == true)
            GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine == true) { 
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1 * Time.deltaTime);

            if (Input.GetButton("Fire1") && reloading == false)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                print("shoot");
                Rigidbody bullet;
                if (type == "ice") { 
                    bullet = PhotonNetwork.Instantiate("Soul Summoner\\Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
                    print(type);
                }
                else
                    bullet = PhotonNetwork.Instantiate("Soul Summoner\\Fire Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
                bullet.AddForce(Camera.main.transform.forward * 100);
                reloading = true;
                StartCoroutine(ExecuteAfterTime(2));
            }

        }
        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
    }

    public void Changetype(string type)
    {
        this.type = type;
    }

    public void CreateShield()
    {
        SoulGameGamager.Instance.shield.SetActive(true);
        print("shield on");
    }

    public void RemoveShield()
    {
        SoulGameGamager.Instance.shield.SetActive(false);
        print("shield off");
    }

}