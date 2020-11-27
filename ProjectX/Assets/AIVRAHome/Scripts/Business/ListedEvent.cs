using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListedEvent : MonoBehaviour
{
    [SerializeField] private BusinessController businessController;
    public Text intName, extName, amount;
    public string eventType;

    void Start()
    {
        businessController = GameObject.Find("Business").GetComponent<BusinessController>();
    }

    public void FocusEventOnClick() {
        var focusedEventController = FindObjectOfType(typeof(FocusedEventController)) as FocusedEventController;
        List<string> dataList = new List<string>();
        switch (eventType)
        {
            case "Deal":
                foreach (Deal deal in businessController.deals) {
                    if (deal.internalName == intName.text) {
                        dataList.Add(deal.externalName);
                        dataList.Add(deal.category);
                        dataList.Add(deal.discountAmount);
                        dataList.Add(deal.audience);
                        dataList.Add(deal.expiry);
                    }
                }
                break;
            case "Reward":
                foreach (Reward reward in businessController.rewards) {
                    if (reward.internalName == intName.text) {
                        dataList.Add(reward.externalName);
                        dataList.Add(reward.gameTitle);
                        dataList.Add(reward.amountOrItem);
                        dataList.Add(reward.requiredScore);
                        dataList.Add(reward.expiry);
                    }
                }
                break;
            case "Game":
                foreach (HostedGame game in businessController.hostedGames) {
                    if (game.eventName == intName.text) {
                        dataList.Add(game.eventName);
                        dataList.Add(game.gameTitle);
                        dataList.Add(game.desc);
                        dataList.Add(game.endDate);
                    }
                }
                break;
            case "Event":
                foreach (HostedEvent evnt in businessController.hostedEvents) { //event is reserved, use evnt
                    if (evnt.eventName == intName.text) {
                        dataList.Add(evnt.eventName);
                        dataList.Add(evnt.eventTitle);
                        dataList.Add(evnt.desc);
                        dataList.Add(evnt.endDate);
                    }
                }
                break;
        }
        focusedEventController.PopulateEvent(eventType, dataList);
    }
}
