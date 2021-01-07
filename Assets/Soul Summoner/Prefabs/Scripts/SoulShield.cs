using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulShield : SoulUnit
{
    // Start is called before the first frame update
    public GameObject shield;
    public SoulAvatar sa;
    Button button;
    public override void Start()
    {
        //base.Start();
        photonview = sa.GetComponent<PhotonView>();
        maxMana = currentMana = 100;
        maxHp = currentHp = 100;
       
        

        if(photonview.IsMine == true)
        {
            button = SoulGameGamager.Instance.shieldButton;

            hpBar = button.gameObject.GetComponent<SoulButtonManager>().active;

            hpRegen = 10;

         //   

        }
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    public override void UseHp(int amount)
    {
        if (currentHp - amount <= 0)
        {
            currentHp = 0;
            StartCoroutine(HpPerSecond(hpRegen, 1 + (maxHp / hpRegen)));
            StartCoroutine(CooldownForButton(button.gameObject, 1 + (maxHp / hpRegen)));
        }
        else
            currentHp -= amount;
        if (photonview.IsMine == true)
        {
            hpBar.fillAmount = (float)currentHp / maxHp;
          //  hpText.text = currentHp + "/" + maxHp;
        }
    }

    public override IEnumerator HpPerSecond(int amount, int duration)  //adds hp per second
    {
        while (duration != 0)
        {
            if (this.gameObject == null)
                break;
            if (currentHp + amount <= 0)
            {
                currentHp = 0;
                StartCoroutine(HpPerSecond(hpRegen, 1 + (maxHp / hpRegen)));
                StartCoroutine(CooldownForButton(button.gameObject, 1 + (maxHp / hpRegen)));
            }
            else
                if (currentHp + amount < 100)
                currentHp += amount;
            else
                currentHp = 100;
            duration--;
            if (photonview.IsMine == true)
            {
                hpBar.fillAmount = (float)currentHp / maxHp;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CooldownForButton(GameObject button, float time)
    {
        if (photonview.IsMine == true)
        {
            
            button.SetActive(false);
            shield.SetActive(false);

           yield return new WaitForSeconds(time);
            

            button.SetActive(true);
        }
    }
}
