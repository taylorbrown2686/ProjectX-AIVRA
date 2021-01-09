using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SoulGameManager : MonoBehaviour
{
    private static SoulGameManager _instance;
    public CreateObject createObject;
    public Button groundShieldButton, iceButton, fireButton, shieldButton, SummonButton, MonsterAttackButton,thunderButton;
    public SoulAvatar sa;
    public Image manaBar;
    public Text manaText;
    public Image hpBar;
    public Text hpText;
    public Image monsterBar;
    public GameObject playerUI;
    public GameObject scrollview, scrollview2;
    public ARRaycastManager arRaycastManager;
    public string spellsFilePath = "Soul Summoner\\KriptoFX\\Realistic Effects Pack v1\\Prefabs\\Effects\\";
    //  public GameObject shield;
    private SoulGameManager()
    {


    }

    public static SoulGameManager Instance
    {
        get { return _instance; }
    }


    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
            Input.location.Start();

            print("buttons get active");
            scrollview.SetActive(true);
            scrollview2.SetActive(true);
            iceButton.gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);
            fireButton.gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);
            shieldButton.gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);
            SummonButton.gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);
            playerUI.SetActive(true);
            monsterBar.gameObject.transform.parent.gameObject.SetActive(true);
        }
    }


    public void SummonMonster()
    {
       // iceButton.gameObject.SetActive(false);
      //  fireButton.gameObject.SetActive(false);
     //   shieldButton.gameObject.SetActive(false);
     //   SummonButton.gameObject.SetActive(false);
        /*Vector3 pos = new Vector3(sa.monsterposition.position.x, 0, sa.monsterposition.position.z);
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
            PhotonNetwork.Instantiate("Soul Summoner\\Monster", pos, Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Soul Summoner\\Perderos", pos, Quaternion.identity);*/
        createObject.gameObject.SetActive(true);

    }

    public void BringBackButtons()
    {
    //    iceButton.gameObject.SetActive(true);
    //    fireButton.gameObject.SetActive(true);
    //    shieldButton.gameObject.SetActive(true);
    }

    public void ShowMonsterAttackButton()
    {
    //    MonsterAttackButton.gameObject.SetActive(true);
    }

}
