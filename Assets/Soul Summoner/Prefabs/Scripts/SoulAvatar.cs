using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SoulAvatar : SoulUnit, IPunObservable
{
    GameObject gameZone;
    
    GameObject parent;
    public GameObject chargeBall;
    float FixeScale = 0.3f;
    bool reloading = false, charging=false;
    string type = "ice";
    public GameObject groundShield,shield,fireP,iceP,ThunderP;
    public SyncSoulAvatar ssa;
    public Transform monsterposition;
    
    public int level = 1;
    int button1ManaCost = 10, button2ManaCost = 15, button3ManaCost = 25, shieldManacost = 5, groundShieldManaCost = 15;
    SoulButtonManager[] buttons;
    public GameObject phoenix, dragon;

    public ARRaycastManager arRaycastManager;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        maxMana = currentMana = 100;
        maxHp = currentHp = 100;
        
        gameZone = GameObject.FindGameObjectWithTag("GameZone");
        transform.SetParent(gameZone.transform);
        parent = gameZone;

        if(photonview.Owner == PhotonNetwork.MasterClient) { 
            phoenix.SetActive(false);
            dragon.SetActive(true);
        }

        if (photonview.IsMine == true) {

            arRaycastManager = SoulGameManager.Instance.arRaycastManager;

            manaRegen = 50;
            hpRegen = 1;
            hpText = SoulGameManager.Instance.hpText;
            hpBar = SoulGameManager.Instance.hpBar;

            //shield starts

            EventTrigger trigger = SoulGameManager.Instance.shieldButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { RemoveShield(); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { CreateShield(); });
            trigger.triggers.Add(entry);
            SoulGameManager.Instance.sa = this;

            //shield ends

            //ground shield starts

            trigger = SoulGameManager.Instance.groundShieldButton.GetComponent<EventTrigger>();
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { CreateGroundSpell(); });
            trigger.triggers.Add(entry);

            //shield ends

            AddCharging(SoulGameManager.Instance.fireButton,"Button1");
            AddCharging(SoulGameManager.Instance.iceButton, "Button2");
            AddCharging(SoulGameManager.Instance.thunderButton, "Button3");

            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(ManaPerSecond(manaRegen , this.gameObject)); // manaregen
            StartCoroutine(HpPerSecond(hpRegen, -1));
            buttons = new SoulButtonManager[5];
            buttons[0] = SoulGameManager.Instance.fireButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[0].SetCost(button1ManaCost);
            buttons[1] = SoulGameManager.Instance.iceButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[1].SetCost(button2ManaCost);
            buttons[2] = SoulGameManager.Instance.shieldButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[2].SetCost(shieldManacost);
            buttons[3] = SoulGameManager.Instance.thunderButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[3].SetCost(button3ManaCost);
            buttons[4] = SoulGameManager.Instance.groundShieldButton.gameObject.GetComponent<SoulButtonManager>();
            buttons[4].SetCost(groundShieldManaCost);
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

    public void CreateGroundSpell()
    {
       // print(SoulGameManager.Instance.spellsFilePath);
        //PhotonNetwork.Instantiate(SoulGameManager.Instance.spellsFilePath + "Ground Shield", transform.position, Quaternion.identity);
        if (photonview.IsMine == true)
            if (UseMana(groundShieldManaCost) == false)
                return;
        print(Screen.currentResolution.width + " " + Screen.currentResolution.height);
        if (arRaycastManager.Raycast(new Vector2(Screen.currentResolution.width/2, Screen.currentResolution.height/2), arRaycastHits))
        {
            var pose = arRaycastHits[0].pose;

            GameObject effect = PhotonNetwork.Instantiate(SoulGameManager.Instance.spellsFilePath + "Ground Shield", pose.position, Quaternion.identity);

            effect.transform.SetParent(gameZone.transform);

            return;
        }
        StartCoroutine(CooldownForButton(SoulGameManager.Instance.iceButton.gameObject, 2));
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
        
        yield return new WaitForSeconds(1f);
        level++;
        }
    }

    public void EndCharge()
    {
        charging = false;
        chargeBall.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator CooldownForButton(GameObject button, float time)
    {
        if (photonview.IsMine == true)
        {
            button.SetActive(false);
            Image active = button.GetComponent<SoulButtonManager>().active;

            int slices = 100;
            for (int i = 0; i <= slices; i++) { 
                active.fillAmount = (float)i / slices;
                yield return new WaitForSeconds(time/ slices);
            }

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
            StartCoroutine(CooldownForButton(SoulGameManager.Instance.fireButton.gameObject, 2));
    }

    public void Button2(int level)
    {
        if (photonview.IsMine == true)
            if (UseMana(button2ManaCost) == false)
                return;
            ssa.fireButton2();
            GameObject effect = Instantiate(iceP, transform.position, transform.rotation);
            effect.GetComponentInChildren<RFX1_TransformMotion>().Speed = 5 * level;
            StartCoroutine(CooldownForButton(SoulGameManager.Instance.iceButton.gameObject, 2));
    }

    public void Button3(int level)
    {
        if (photonview.IsMine == true)
            if (UseMana(button3ManaCost) == false)
                return;
        ssa.fireButton3();
        GameObject effect;
        for (int i = 0; i < level; i++) { 
            effect = Instantiate(ThunderP, transform.position, transform.rotation);
            effect.GetComponentInChildren<RFX1_TransformMotion>().RandomMoveRadius = i - 1;
        }
        StartCoroutine(CooldownForButton(SoulGameManager.Instance.thunderButton.gameObject, 5));
    }

    public IEnumerator ManaPerSecond(int amount, GameObject gameObject) //adds mana per second
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
                SoulGameManager.Instance.manaBar.fillAmount = (float)currentMana / maxMana;
                SoulGameManager.Instance.manaText.text = currentMana + "/" + maxMana;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}