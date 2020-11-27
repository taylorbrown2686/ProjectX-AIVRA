using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ViewEventsPage : MonoBehaviour
{
    [SerializeField] private Dropdown eventFilter;
    [SerializeField] private ScrollView eventList;
    [SerializeField] private GameObject listedEvent;
    [SerializeField] private RectTransform scrollViewContent;
    [SerializeField] private BusinessController businessController;
    private int listedEventCount = 0;

    private void OnEnable()
    {
        OnEventFilterChanged();
    }

    private void Update()
    {
        if (scrollViewContent.offsetMin.y < 0) {
            scrollViewContent.offsetMin = new Vector2(0, 0); 
            scrollViewContent.offsetMax = new Vector2(0, 0);
        }
        if (scrollViewContent.offsetMin.y > 100 * (listedEventCount - 4)) { 
            scrollViewContent.offsetMin = new Vector2(0, 100 * (listedEventCount - 4)); 
            scrollViewContent.offsetMax = new Vector2(0, 100 * (listedEventCount - 4));
        }
    }

    public void OnEventFilterChanged() {
        foreach (Transform child in scrollViewContent.transform) {
            Destroy(child.gameObject);
        }
        businessController.RepullEvents();
        switch (eventFilter.options[eventFilter.value].text) {
            case "Deals":
                for (int i = 0; i < businessController.deals.Count; i++) {
                    var newDeal = Instantiate(listedEvent, Vector3.zero + new Vector3(0, -100f * i, 0), Quaternion.identity);
                    newDeal.transform.SetParent(scrollViewContent.transform, false);
                    var newListedEvent = newDeal.GetComponent<ListedEvent>();
                    newListedEvent.intName.text = businessController.deals[i].internalName;
                    newListedEvent.extName.text = businessController.deals[i].externalName;
                    newListedEvent.amount.text = businessController.deals[i].discountAmount;
                    newListedEvent.eventType = "Deal";
                }
                listedEventCount = businessController.deals.Count;
            break;
            case "Rewards":
                for (int i = 0; i < businessController.rewards.Count; i++) {
                    var newReward = Instantiate(listedEvent, Vector3.zero + new Vector3(0, -100f * i, 0), Quaternion.identity);
                    newReward.transform.SetParent(scrollViewContent.transform, false);
                    var newListedEvent = newReward.GetComponent<ListedEvent>();
                    newListedEvent.intName.text = businessController.rewards[i].internalName;
                    newListedEvent.extName.text = businessController.rewards[i].externalName;
                    newListedEvent.amount.text = businessController.rewards[i].amountOrItem;
                    newListedEvent.eventType = "Reward";
                }
                listedEventCount = businessController.rewards.Count;
            break;
            case "Games":
                for (int i = 0; i < businessController.hostedGames.Count; i++) {
                    var newGame = Instantiate(listedEvent, Vector3.zero + new Vector3(0, -100f * i, 0), Quaternion.identity);
                    newGame.transform.SetParent(scrollViewContent.transform, false);
                    var newListedEvent = newGame.GetComponent<ListedEvent>();
                    newListedEvent.intName.text = businessController.hostedGames[i].eventName;
                    newListedEvent.extName.text = businessController.hostedGames[i].gameTitle;
                    newListedEvent.amount.text = businessController.hostedGames[i].endDate;
                    newListedEvent.eventType = "Game";
                }
                listedEventCount = businessController.hostedGames.Count;
            break;
            case "Events":
                for (int i = 0; i < businessController.hostedEvents.Count; i++) {
                    var newEvent = Instantiate(listedEvent, Vector3.zero + new Vector3(0, -100f * i, 0), Quaternion.identity);
                    newEvent.transform.SetParent(scrollViewContent.transform, false);
                    var newListedEvent = newEvent.GetComponent<ListedEvent>();
                    newListedEvent.intName.text = businessController.hostedEvents[i].eventName;
                    newListedEvent.extName.text = businessController.hostedEvents[i].eventTitle;
                    newListedEvent.amount.text = businessController.hostedEvents[i].endDate;
                    newListedEvent.eventType = "Event";
                }
                listedEventCount = businessController.hostedEvents.Count;
            break;
        }
    }
}
