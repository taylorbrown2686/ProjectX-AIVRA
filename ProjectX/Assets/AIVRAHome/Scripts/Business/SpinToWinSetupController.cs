using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinToWinSetupController : MonoBehaviour
{
    [SerializeField] private InputField[] oddsFields, rewardsFields, maxWinFields;
    [SerializeField] private Text isSpinOnText, totalChanceText, losingChanceText, errorText;
    [SerializeField] private GameObject fieldsCover, saveButtonCover;
    private bool isSpinOn = false;

    private void OnEnable()
    {
        StartCoroutine(CheckIfSpinEnabled());
    }

    public void UpdateChanceText() //Attach to OnEndEdit of input fields (only odds fields)
    {
        float totalChance = 0;
        float losingChance = 0;
        foreach (InputField field in oddsFields)
        {
            float temp = System.Single.Parse(field.text);
            if (temp == 0)
            {
                continue;
            }
            totalChance += (1 / temp) * 100;
        }
        if (totalChance > 100)
        {
            errorText.text = "Your winning odds are over 100%. Please change some of the values.";
            saveButtonCover.SetActive(true);
        }
        else
        {
            errorText.text = "Touch 'Save' to make your changes live.";
            saveButtonCover.SetActive(false);
        }
        losingChance = 100 - totalChance;
        totalChanceText.text = "Total Odds of Winning: " + totalChance + "%";
        losingChanceText.text = "Odds of Losing: " + losingChance + "%";
    }

    public void Back()
    {
        BusinessController.Instance.optionSelectScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private IEnumerator CheckIfSpinEnabled()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/checkIfSpinEnabled.php", form);
        yield return www;
        if (www.text == "1")
        {
            fieldsCover.SetActive(false);
            isSpinOnText.text = "Daily Spin is currently ON in your location";
            isSpinOn = true;
            StartCoroutine(GetOddsAndRewards());
            StartCoroutine(GetWinAmounts());
        }
    }

    public void ToggleSpinOnClick()
    {
        StartCoroutine(ToggleSpin());
    }
    private IEnumerator ToggleSpin()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/toggleSpinToWin.php", form);
        yield return www;
        if (isSpinOn)
        {
            isSpinOnText.text = "Daily Spin is currently OFF in your location";
            fieldsCover.SetActive(true);
            isSpinOn = false;
        }
        else
        {
            isSpinOnText.text = "Daily Spin is currently ON in your location";
            fieldsCover.SetActive(false);
            isSpinOn = true;
        }
    }

    private IEnumerator GetOddsAndRewards()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/pullOddsAndRewards.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < oddsFields.Length + rewardsFields.Length; i += 2)
        {
            oddsFields[i / 2].text = splitString[i];
            rewardsFields[i / 2].text = splitString[i + 1];
        }
        UpdateChanceText();
    }

    private IEnumerator GetWinAmounts()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/pullMaxWinAmounts.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        for (int i = 0; i < maxWinFields.Length; i++)
        {
            maxWinFields[i].text = splitString[i];
        }
    }

    public void OnSave()
    {
        foreach (InputField field in oddsFields)
        {
            if (field.text == "")
            {
                field.text = "0";
            }
            if (field.text.Contains("-"))
            {
                errorText.text = "You cannot have negative odds.";
                return;
            }
        }
        foreach (InputField field in rewardsFields)
        {
            if (field.text == "")
            {
                field.text = "Free Spin";
            }
        }
        foreach (InputField field in maxWinFields)
        {
            if (field.text == "")
            {
                field.text = "999";
            }
            if (field.text.Contains("-"))
            {
                errorText.text = "You cannot give out negative rewards.";
                return;
            }
        }
        StartCoroutine(UpdateOddsAndRewards());
    }

    private IEnumerator UpdateOddsAndRewards()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("barOdds", oddsFields[0].text);
        form.AddField("barReward", rewardsFields[0].text);
        form.AddField("starOdds", oddsFields[1].text);
        form.AddField("starReward", rewardsFields[1].text);
        form.AddField("sevenOdds", oddsFields[2].text);
        form.AddField("sevenReward", rewardsFields[2].text);
        form.AddField("orangeOdds", oddsFields[3].text);
        form.AddField("orangeReward", rewardsFields[3].text);
        form.AddField("grapeOdds", oddsFields[4].text);
        form.AddField("grapeReward", rewardsFields[4].text);
        form.AddField("bellOdds", oddsFields[5].text);
        form.AddField("bellReward", rewardsFields[5].text);
        form.AddField("cherryOdds", oddsFields[6].text);
        form.AddField("cherryReward", rewardsFields[6].text);
        form.AddField("diamondOdds", oddsFields[7].text);
        form.AddField("diamondReward", rewardsFields[7].text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/updateOddsAndRewards.php", form);
        yield return www;
        StartCoroutine(UpdateMaxWinsAmount());
    }

    private IEnumerator UpdateMaxWinsAmount()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        form.AddField("barWinMax", maxWinFields[0].text);
        form.AddField("starWinMax", maxWinFields[1].text);
        form.AddField("sevenWinMax", maxWinFields[2].text);
        form.AddField("orangeWinMax", maxWinFields[3].text);
        form.AddField("grapeWinMax", maxWinFields[4].text);
        form.AddField("bellWinMax", maxWinFields[5].text);
        form.AddField("cherryWinMax", maxWinFields[6].text);
        form.AddField("diamondWinMax", maxWinFields[7].text);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/updateSpinMaxWin.php", form);
        yield return www;
        errorText.text = "Changes saved!";
    }
}
