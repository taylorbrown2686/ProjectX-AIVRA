using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulUnit : MonoBehaviour
{
    public int maxMana, currentMana;
    public int maxHp, currentHp;
    protected PhotonView photonview;
    public Text hpText;
    public Image hpBar;
    protected int hpRegen, manaRegen;

    // Start is called before the first frame update
    public virtual void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool UseMana(int amount)
    {
        if (currentMana - amount < 0)
        {
            return false;
        }
        currentMana -= amount;
        if (photonview.IsMine == true && this.gameObject.tag == "avatar")
        {
            SoulGameManager.Instance.manaBar.fillAmount = (float)currentMana / maxMana;
            SoulGameManager.Instance.manaText.text = currentMana + "/" + maxMana;
        }
        return true;

    }

    public virtual void UseHp(int amount)
    {
        if (currentHp - amount <= 0)
        {
            Destroy(this.gameObject);
        }
        currentHp -= amount;
        if (photonview.IsMine == true)
        {
            hpBar.fillAmount = (float)currentHp / maxHp;
            hpText.text = currentHp + "/" + maxHp;
        }
    }

    public virtual IEnumerator HpPerSecond(int amount, int duration)  //adds hp per second
    {
        while (duration != 0)
        {
            if (this.gameObject == null)
                break;
            if (currentHp + amount <= 0)
            {
                currentHp = 0;
                Destroy(this.gameObject);
            }
            else
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






}
