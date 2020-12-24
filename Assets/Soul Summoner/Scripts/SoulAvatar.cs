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
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(gameZone.transform);
        parent = gameZone;
        if (photonview.IsMine == true) { 
            SoulGameGamager.Instance.fireButton.onClick.AddListener(delegate { Button1(); });
            SoulGameGamager.Instance.iceButton.onClick.AddListener(delegate { Button2(); });
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

        
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine == true) { 
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1 * Time.deltaTime);
            transform.rotation = Camera.main.transform.rotation;
        }
        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator ExecuteAfterTime(GameObject button, float time)
    {
        button.SetActive(false);
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    public void Changetype(string type)
    {
        this.type = type;
    }

    public void CreateShield()
    {
        shield.SetActive(true);
        print("shield on");
    }

    public void RemoveShield()
    {
        shield.SetActive(false);
        print("shield off");
    }

    public void Button1()
    {
        Rigidbody bullet = PhotonNetwork.Instantiate("Soul Summoner\\Fire Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
        bullet.AddForce(Camera.main.transform.forward * 100);
      
        StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.fireButton.gameObject,2));
    }

    public void Button2()
    {
        Rigidbody bullet = PhotonNetwork.Instantiate("Soul Summoner\\Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
        bullet.AddForce(Camera.main.transform.forward * 100);
       
        StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.iceButton.gameObject,2));
    }

}