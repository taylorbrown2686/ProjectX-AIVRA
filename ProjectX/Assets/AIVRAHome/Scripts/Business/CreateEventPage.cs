using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEventPage : MonoBehaviour
{
    //Generic
    [SerializeField] private Dropdown eventType;
    [SerializeField] private GameObject[] wizards;
    [SerializeField] private BusinessController businessController;
    [SerializeField] private GameObject creatingEventPopup;
    [SerializeField] private Text creatingEventText;

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
        StartCoroutine(CreateInDB(eventType));
        StartCoroutine(CreatingDealPopup(eventType));
    }

    private IEnumerator CreateInDB(string eventType)
    {
        switch (eventType)
        {
            case "Deal":
                /*WWWForm form = new WWWForm();
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
                WWW www = new WWW("http://localhost:8080/AIVRA-PHP/uploadNewDeal.php", form);
                yield return www;
                Debug.Log(www.text);*/
                Debug.Log("hacky");
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
                WWW www2 = new WWW("http://localhost:8080/AIVRA-PHP/uploadNewReward.php", form2);
                yield return www2;
                Debug.Log(www2.text);
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
                WWW www3 = new WWW("http://localhost:8080/AIVRA-PHP/uploadNewHostedGame.php", form3);
                yield return www3;
                Debug.Log(www3.text);
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
                WWW www4 = new WWW("http://localhost:8080/AIVRA-PHP/uploadNewHostedEvent.php", form4);
                yield return www4;
                Debug.Log(www4.text);
                break;
        }
    }

    private IEnumerator CreatingDealPopup(string eventType)
    {
        creatingEventPopup.SetActive(true);
        creatingEventText.text = "";
        creatingEventText.text += "Creating your " + eventType + "...";
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        creatingEventText.text += "\nSaving to database...";
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        creatingEventText.text += "\nDone!";
        yield return new WaitForSeconds(2f);
        creatingEventPopup.SetActive(false);
    }
}
