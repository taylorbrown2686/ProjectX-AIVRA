using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoulAvatar : MonoBehaviour, IPunObservable
{
    GameObject gameZone;
    PhotonView photonview;
    GameObject parent;
    float FixeScale = 0.3f;
    bool reloading = false;
    string type = "ice";
    public GameObject shield,fireP,iceP;
    public SyncSoulAvatar ssa;
    public Transform monsterposition;
    int maxMana, currentMana;
    public int maxHp, currentHp;
    Text hpText;
    Image hpBar;
    int button1ManaCost = 20, button2ManaCost = 30, shieldManacost = 5;
    SoulButtonManager[] buttons;


    // Start is called before the first frame update
    void Start()
    {
        maxMana = currentMana = 100;
        maxHp = currentHp = 100;
        photonview = GetComponent<PhotonView>();
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(gameZone.transform);
        parent = gameZone;
        if (photonview.IsMine == true) {

            hpText = SoulGameGamager.Instance.hpText;
            hpBar = SoulGameGamager.Instance.hpBar;

            SoulGameGamager.Instance.fireButton.onClick.AddListener(delegate { Button1(); });
            SoulGameGamager.Instance.iceButton.onClick.AddListener(delegate { Button2(); });

            EventTrigger trigger = SoulGameGamager.Instance.shieldButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { RemoveShield(); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { CreateShield(); });
            trigger.triggers.Add(entry);
            SoulGameGamager.Instance.sa = this;
        
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(ManaPerSecond(3 , this.gameObject));

            buttons = new SoulButtonManager[3];
            buttons[0] = SoulGameGamager.Instance.fireButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[0].SetCost(button1ManaCost);
            buttons[1] = SoulGameGamager.Instance.iceButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[1].SetCost(button2ManaCost);
            buttons[2] = SoulGameGamager.Instance.shieldButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[2].SetCost(shieldManacost);
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

    private void FixedUpdate()
    {
        if (photonview.IsMine == true)
        {
            foreach (SoulButtonManager sbm in buttons)
            {
                sbm.Check(currentMana);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator ExecuteAfterTime(GameObject button, float time)
    {
        if (photonview.IsMine == true)
        {

            button.SetActive(false);
            yield return new WaitForSeconds(time);
            button.SetActive(true);
        }
    }

    public void Changetype(string type)
    {
        this.type = type;
    }

    public void CreateShield()
    {
        
            shield.SetActive(true);
            print("shield on");
            StartCoroutine(ManaPerSecond(-1 * shieldManacost, shield));
        
    }

    public void RemoveShield()
    {
      
            shield.SetActive(false);
            print("shield off");
        
    }

    public void Button1()
    {
        if (photonview.IsMine == true)
            if (UseMana(button1ManaCost) == false)
                return;
            ssa.fireButton1();
            GameObject effect = Instantiate(fireP, transform.position, transform.rotation);
            StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.fireButton.gameObject, 2));
        
    }

    public void Button2()
    {
        if (photonview.IsMine == true)
            if (UseMana(button2ManaCost) == false)
                return;
            ssa.fireButton2();
            // Rigidbody bullet = PhotonNetwork.Instantiate("Soul Summoner\\Projectile", Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
            //  bullet.AddForce(Camera.main.transform.forward * 100);
            GameObject effect = Instantiate(iceP, transform.position, transform.rotation);

            StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.iceButton.gameObject, 2));
        
       
    }

    bool UseMana(int amount)
    {
        if(currentMana - amount < 0)
        {
            return false;
        }
        currentMana -= amount;
        if (photonview.IsMine == true)
        {
            SoulGameGamager.Instance.manaBar.fillAmount = (float)currentMana / maxMana;
            SoulGameGamager.Instance.manaText.text = currentMana + "/" + maxMana;
        }
        return true;

    }

    public void UseHp(int amount)
    {
        if (currentHp - amount <= 0)
        {
            Destroy(this.gameObject);
        }
        currentHp -= amount;
        if (photonview.IsMine == true) { 
                
            hpBar.fillAmount = (float)currentHp / maxHp;
            hpText.text = currentHp + "/" + maxHp;
        }
    }

    IEnumerator ManaPerSecond(int amount, GameObject gameObject)
    {
        while (gameObject.activeSelf == true)
        {
            if(currentMana + amount < 0)
            {
                gameObject.SetActive(false);
                break;
            }
            if (currentMana + amount <= 100)
                currentMana += amount;
            else
                currentMana = 100;
            if (photonview.IsMine == true)
            {
                SoulGameGamager.Instance.manaBar.fillAmount = (float)currentMana / maxMana;
                SoulGameGamager.Instance.manaText.text = currentMana + "/" + maxMana;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}