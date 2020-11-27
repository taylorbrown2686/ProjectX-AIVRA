using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CreateEventPage : MonoBehaviour
{
    //Generic
    [SerializeField] private Dropdown eventType;
    [SerializeField] private GameObject[] wizards;
    [SerializeField] private BusinessController businessController;
    [SerializeField] private GameObject creatingEventPopup;
    [SerializeField] private Text creatingEventText, errorText;

    //Deal eventType
    [SerializeField] private Text audienceText, discountText;
    private string currentAudience, currentDiscountType;
    //DB Fields
    [SerializeField] private InputField dealPrivateName, dealPublicName, dealTags, discountAmount, dealExpiry, dealAmt, dealOrigPrice;
    [SerializeField] private Dropdown dealCategory;
    //[SerializeField] private Text amtDist, ourCut;

    //Reward eventType
    [SerializeField] private Text typeOfRewardText;
    private string currentRewardType;
    //DB Fields
    [SerializeField] private InputField rewardPrivateName, rewardPublicName, rewardTags, howMuch, scoreRequired, rewardExpiry, rewardAmt;
    [SerializeField] private Dropdown rewardCategory;

    //Game eventType
    //DB Fields
    [SerializeField] private InputField gameEventName, gameTags, gameDesc, gameSubDesc, gameEndOf;
    [SerializeField] private Dropdown gameCategory;

    //Event eventType
    //DB Fields
    [SerializeField] private InputField eventEventName, eventTags, eventDesc, eventSubDesc, eventEndOf;
    [SerializeField] private Dropdown eventCategory;

    void OnEnable()
    {
        creatingEventPopup.SetActive(false);
        EventTypeChanged();
        currentAudience = "All Users";
        currentDiscountType = "Percentage";
        currentRewardType = "Cash";
    }

    public void EventTypeChanged()
    {
        DisableAllWizards();
        wizards[eventType.value].SetActive(true);
    }

    private void DisableAllWizards()
    {
        foreach (GameObject obj in wizards)
        {
            obj.SetActive(false);
        }
    }
    #region DealMethods
    public void AudienceChanged(string audience)
    {
        audienceText.text = "Audience: " + audience;
        currentAudience = audience;
    }

    public void DiscountChanged(string discountType)
    {
        discountText.text = "Discount: " + discountType;
        currentDiscountType = discountType;
    }
    #endregion
    #region RewardMethods
    public void RewardTypeChanged(string rewardType)
    {
        typeOfRewardText.text = "Type Of Reward: " + rewardType;
        currentRewardType = rewardType;
    }
    #endregion


    public void Create(string eventType)
    {
        if (VerifyFields(eventType) == "No Errors")
        {
            StartCoroutine(CheckForDuplicates(eventType));
        } else
        {
            errorText.text = VerifyFields(eventType);
        }
    }

    private string VerifyFields(string eventType)
    {
        DateTime date;
        switch (eventType)
        {
            case "Deal":
                if (dealPrivateName.text == "" || dealPublicName.text == "" || dealTags.text == "" || discountAmount.text == "" || dealAmt.text == "" || dealOrigPrice.text == "")
                {
                    return "A field was left blank.";
                }
                if (dealExpiry.text == "")
                {
                    dealExpiry.text = "01/01/2050";
                }
                if (!Regex.IsMatch(dealPublicName.text, "^[a-zA-Z0-9 ]*$") || !Regex.IsMatch(dealPrivateName.text, "^[a-zA-Z0-9]*$"))
                {
                    return "Please avoid symbols in the internal and external name. Avoid spaces in the internal name.";
                }
                if (currentDiscountType == "Percentage")
                {
                    if (float.Parse(discountAmount.text) > 100)
                    {
                        return "You cannot discount over 100%.";
                    }
                } else
                {
                    if (float.Parse(discountAmount.text) > float.Parse(dealOrigPrice.text))
                    {
                        return "You cannot discount the full price.";
                    }
                }
                if (!DateTime.TryParse(dealExpiry.text, out date))
                {
                    return "Your expiration was invalid, use format MM/DD/YYYY";
                }
                break;
            case "Reward":
                if (rewardPrivateName.text == "" || rewardPublicName.text == "" || rewardTags.text == "" || howMuch.text == "" || scoreRequired.text == "" || rewardExpiry.text == "" || rewardAmt.text == "")
                {
                    return "A field was left blank.";
                }
                if (rewardExpiry.text == "")
                {
                    rewardExpiry.text = "01/01/2050";
                }
                if (!Regex.IsMatch(rewardPrivateName.text, "^[a-zA-Z0-9]*$") || !Regex.IsMatch(rewardPublicName.text, "^[a-zA-Z0-9 ]*$"))
                {
                    return "Please avoid symbols in the internal and external name. Avoid spaces in the internal name.";
                }
                if (!DateTime.TryParse(rewardExpiry.text, out date))
                {
                    return "Your expiration was invalid, use format MM/DD/YYYY";
                }
                break;
            case "Game":
                if (gameEventName.text == "" || gameTags.text == "" || gameDesc.text == "" || gameSubDesc.text == "")
                {
                    return "A field was left blank.";
                }
                if (gameEndOf.text == "")
                {
                    gameEndOf.text = "01/01/2050";
                }
                if (!Regex.IsMatch(gameEventName.text, "^[a-zA-Z0-9 ]*$"))
                {
                    return "Please avoid symbols in the event name.";
                }
                if (!DateTime.TryParse(gameEndOf.text, out date))
                {
                    return "Your expiration was invalid, use format MM/DD/YYYY";
                }
                break;
            case "Event":
                if (eventEventName.text == "" || eventTags.text == "" || eventDesc.text == "" || eventSubDesc.text == "")
                {
                    return "A field was left blank.";
                }
                if (eventEndOf.text == "")
                {
                    eventEndOf.text = "01/01/2050";
                }
                if (!Regex.IsMatch(eventEventName.text, "^[a-zA-Z0-9 ]*$"))
                {
                    return "Please avoid symbols in the event name.";
                }
                if (!DateTime.TryParse(eventEndOf.text, out date))
                {
                    return "Your expiration was invalid, use format MM/DD/YYYY";
                }
                break;
        }
        return "No Errors";
    }

    private IEnumerator CheckForDuplicates(string eventType)
    {
        //if no dupes
        StartCoroutine(CreateInDB(eventType));
        StartCoroutine(CreatingDealPopup(eventType));
        yield return null;
    }

    private IEnumerator CreateInDB(string eventType)
    {
        switch (eventType)
        {
            case "Deal":
                WWWForm form = new WWWForm();
                form.AddField("businessName", businessController.businessName);
                form.AddField("internalName", dealPrivateName.text);
                form.AddField("externalName", dealPublicName.text);
                form.AddField("category", dealCategory.options[dealCategory.value].text);
                form.AddField("tags", dealTags.text);
                form.AddField("audience", currentAudience);
                form.AddField("discountType", currentDiscountType);
                form.AddField("discountAmount", discountAmount.text);
                form.AddField("expiry", dealExpiry.text);
                form.AddField("amountDistributed", dealAmt.text);
                form.AddField("priceOfDiscountedItem", dealOrigPrice.text);
                WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/uploadNewDeal.php", form);
                yield return www;
                break;

            case "Reward":
                WWWForm form2 = new WWWForm();
                form2.AddField("businessName", businessController.businessName);
                form2.AddField("internalName", rewardPrivateName.text);
                form2.AddField("externalName", rewardPublicName.text);
                form2.AddField("gameTitle", rewardCategory.options[rewardCategory.value].text);
                form2.AddField("tags", rewardTags.text);
                form2.AddField("typeOfReward", currentRewardType);
                form2.AddField("amountOrItem", howMuch.text);
                form2.AddField("requiredScore", scoreRequired.text);
                form2.AddField("expiry", rewardExpiry.text);
                form2.AddField("amountDistributed", rewardAmt.text);
                WWW www2 = new WWW("http://65.52.195.169/AIVRA-PHP/uploadNewReward.php", form2);
                yield return www2;
                break;

            case "Game":
                WWWForm form3 = new WWWForm();
                form3.AddField("businessName", businessController.businessName);
                form3.AddField("eventName", gameEventName.text);
                form3.AddField("gameTitle", gameCategory.options[gameCategory.value].text);
                form3.AddField("tags", gameTags.text);
                form3.AddField("description", gameDesc.text);
                form3.AddField("subDescription", gameSubDesc.text);
                form3.AddField("endDate", gameEndOf.text);
                WWW www3 = new WWW("http://65.52.195.169/AIVRA-PHP/uploadNewHostedGame.php", form3);
                yield return www3;
                break;

            case "Event":
                WWWForm form4 = new WWWForm();
                form4.AddField("businessName", businessController.businessName);
                form4.AddField("eventName", eventEventName.text);
                form4.AddField("eventCategory", eventCategory.options[eventCategory.value].text);
                form4.AddField("tags", eventTags.text);
                form4.AddField("description", eventDesc.text);
                form4.AddField("subDescription", eventSubDesc.text);
                form4.AddField("endDate", eventEndOf.text);
                WWW www4 = new WWW("http://65.52.195.169/AIVRA-PHP/uploadNewHostedEvent.php", form4);
                yield return www4;
                break;
        }
    }

    private IEnumerator CreatingDealPopup(string eventType)
    {
        creatingEventPopup.SetActive(true);
        creatingEventText.text = "";
        creatingEventText.text += "Creating your " + eventType + "...";
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
        creatingEventText.text += "\nSaving to database...";
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
        creatingEventText.text += "\nDone!";
        yield return new WaitForSeconds(2f);
        creatingEventPopup.SetActive(false);
    }
}
