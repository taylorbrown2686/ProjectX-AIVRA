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
    public GameObject chargeBall;
    float FixeScale = 0.3f;
    bool reloading = false, charging=false;
    string type = "ice";
    public GameObject shield,fireP,iceP,ThunderP;
    public SyncSoulAvatar ssa;
    public Transform monsterposition;
    int maxMana, currentMana;
    public int maxHp, currentHp;
    Text hpText;
    Image hpBar;
    public int level = 1;
    int button1ManaCost = 20, button2ManaCost = 30, button3ManaCost = 50, shieldManacost = 5;
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

        //    SoulGameGamager.Instance.fireButton.onClick.AddListener(delegate { Button1(); });
        //    SoulGameGamager.Instance.iceButton.onClick.AddListener(delegate { Button2(); });
        //    SoulGameGamager.Instance.thunderButton.onClick.AddListener(delegate { Button3(); });


            //shield

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

            //shield end

            AddCharging(SoulGameGamager.Instance.fireButton,"Button1");
            AddCharging(SoulGameGamager.Instance.iceButton, "Button2");
            AddCharging(SoulGameGamager.Instance.thunderButton, "Button3");

            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(ManaPerSecond(3 , this.gameObject));

            buttons = new SoulButtonManager[4];
            buttons[0] = SoulGameGamager.Instance.fireButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[0].SetCost(button1ManaCost);
            buttons[1] = SoulGameGamager.Instance.iceButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[1].SetCost(button2ManaCost);
            buttons[2] = SoulGameGamager.Instance.shieldButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[2].SetCost(shieldManacost);
            buttons[3] = SoulGameGamager.Instance.thunderButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[3].SetCost(button3ManaCost);
        }
    }


    void AddCharging(Button button, string methodName)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { EndCharge(); });
        //trigger.triggers.Add(entry);
        switch (methodName) {
            case "Button1":
                entry.callback.AddListener((data) => { Button1(level); });
                trigger.triggers.Add(entry);
                break;
            case "Button2":
                entry.callback.AddListener((data) => { Button2(level); });
                trigger.triggers.Add(entry);
                break;
            case "Button3":
                entry.callback.AddListener((data) => { Button3(level); });
                trigger.triggers.Add(entry);
                break;
        }   

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { StartCharge(); });
        trigger.triggers.Add(entry);
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

    public void StartCharge()
    {
        //add return if charging is already true
        chargeBall.SetActive(true);
        charging = true;
        StartCoroutine(ChargeLevel());
    }

    IEnumerator ChargeLevel()
    {
        level = 1;
        while(charging == true) {
        print(level);
        
        yield return new WaitForSeconds(1);
        level++;
        }
    }

    public void EndCharge()
    {
        print("endddddddddddddddddddddddddddddddd");
        charging = false;
        chargeBall.SetActive(false);
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

    public void Button1(int level)
    {
        if (photonview.IsMine == true)
            if (UseMana(button1ManaCost) == false)
                return;
            ssa.fireButton1();
            GameObject effect = Instantiate(fireP, transform.position, transform.rotation);
            effect.transform.localScale *= level;
            StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.fireButton.gameObject, 2));
    }

    public void Button2(int level)
    {
        if (photonview.IsMine == true)
            if (UseMana(button2ManaCost) == false)
                return;
            ssa.fireButton2();
            GameObject effect = Instantiate(iceP, transform.position, transform.rotation);
            effect.GetComponentInChildren<RFX1_TransformMotion>().Speed = 5 * level;
            StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.iceButton.gameObject, 2));
    }

    public void Button3(int level)
    {
        if (photonview.IsMine == true)
            if (UseMana(button3ManaCost) == false)
                return;
        ssa.fireButton3();
        GameObject effect = Instantiate(ThunderP, transform.position, transform.rotation);
        StartCoroutine(ExecuteAfterTime(SoulGameGamager.Instance.thunderButton.gameObject, 2));

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

    public IEnumerator HpPerSecond(int amount,int duration)
    {
        while (duration > 0)
        {
            if (this.gameObject == null)
                break;
            if (currentHp + amount < 0)
            {
                currentHp = 0;
                Destroy(this.gameObject);
            }else
                if (currentHp + amount <= 100)
                    currentHp += amount;
                else
                    currentHp = 100;
            duration--;
            if (photonview.IsMine == true)
            {
                hpBar.fillAmount = (float)currentHp / maxHp;
                hpText.text = currentHp + "/" + maxHp;
            }
            yield return new WaitForSeconds(1f);
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