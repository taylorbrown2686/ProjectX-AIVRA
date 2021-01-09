using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardsController : MonoBehaviour
{
    [SerializeField] private string gameTitle;
    [SerializeField] private GameObject youWonImage;
    [SerializeField] private Transform canvas;
    private List<Reward> rewards = new List<Reward>();
    private bool givingReward = false;
    private GameObject winObj;
    private Reward wonReward;

    private static RewardsController instance = null;

    public static RewardsController Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine(GetRewards());
    }

    private IEnumerator GetRewards()
    {
        WWWForm form = new WWWForm();
        form.AddField("gameTitle", gameTitle);
        form.AddField("businessName", CrossSceneVariables.Instance.inBusinessName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getRewardsFromBusinessName.php", form);
        yield return www;
        string[] splitString = www.text.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < splitString.Length; i+=7)
        {
            Reward reward = new Reward();
            reward.businessName = splitString[i];
            reward.internalName = splitString[i + 1];
            reward.gameTitle = splitString[i + 2];
            reward.typeOfReward = splitString[i + 3];
            reward.amountOrItem = splitString[i + 4];
            reward.requiredScore = splitString[i + 5];
            reward.expiry = splitString[i + 6];
            rewards.Add(reward);
        }
    }

    public void CompareScore(int scoreToCheck)
    {
        foreach (Reward reward in rewards)
        {
            if (Convert.ToInt32(reward.requiredScore) < scoreToCheck)
            {
                if (!givingReward)
                {
                    StartCoroutine(CheckIfRewardWonAlready(reward));
                }
            }
        }
    }

    private IEnumerator CheckIfRewardWonAlready(Reward reward)
    {
        givingReward = true;
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("internalname", reward.internalName);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkIfDealIsUsed.php", form);
        yield return www;
        if (www.text == "0" || www.text == "1")
        {
            givingReward = false;
            yield break; //won already
        } 
        else
        {
            wonReward = reward;
            StartCoroutine(WinAnimation(CrossSceneVariables.Instance.inBusinessName, reward.amountOrItem, reward.expiry));
        }
    }

    private IEnumerator GiveReward(Reward reward)
    {
        StartCoroutine(WinAnimation(CrossSceneVariables.Instance.inBusinessName, reward.amountOrItem, reward.expiry));
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("dealInternalName", reward.internalName);
        form.AddField("dealIssuer", CrossSceneVariables.Instance.inBusinessName);
        form.AddField("dealDiscount", reward.amountOrItem);
        form.AddField("dealExpiry", reward.expiry);
        form.AddField("isReward", 1);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/saveDealToCustomerAccount.php", form);
        yield return www;
    }

    private IEnumerator WinAnimation(string issuer, string amount, string expiry)
    {
        winObj = Instantiate(youWonImage, Vector3.zero, Quaternion.identity);
        //winObj.GetComponent<DealCouponPopulator>().
        winObj.transform.SetParent(canvas, false);
        winObj.transform.localScale = new Vector3(0, 0, 0);
        while (winObj.transform.localScale.x < 0.5f)
        {
            winObj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Transform child in winObj.transform)
        {
            if (child.gameObject.name == "Yes")
            {
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Yes(); });
            }
            if (child.gameObject.name == "No")
            {
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { No(); });
            }
        }
        Time.timeScale = 0;
    }

    private IEnumerator ReverseWinAnimation()
    {
        while (winObj.transform.localScale.x > 0)
        {
            winObj.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(winObj);
    }

    public void Yes() //yes, go to spin
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SpinToWin");
    }

    public void No() //no, don't go to spin
    {
        Time.timeScale = 1;
        StartCoroutine(ReverseWinAnimation());
        StartCoroutine(GiveReward(wonReward));
    }
}
