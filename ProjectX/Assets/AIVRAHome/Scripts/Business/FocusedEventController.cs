using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusedEventController : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen, dealScreen;
    [SerializeField] private Text eventTitle, eventCategory, eventDesc, eventExpiry;
    [SerializeField] private Text dealTitle, dealCategory, amountText, audienceText, dealExpiry;

    public void PopulateEvent(string eventType, List<string> eventData)
    {
        switch (eventType)
        {
            case "Deal":
                gameScreen.SetActive(false);
                dealScreen.SetActive(true);
                dealTitle.text = eventData[0];
                dealCategory.text = eventData[1];
                amountText.text = eventData[2];
                audienceText.text = eventData[3];
                dealExpiry.text = eventData[4];
                break;
            case "Reward":
                gameScreen.SetActive(false);
                dealScreen.SetActive(true);
                dealTitle.text = eventData[0];
                dealCategory.text = eventData[1];
                amountText.text = eventData[2];
                audienceText.text = eventData[3];
                dealExpiry.text = eventData[4];
                break;
            case "Game":
                gameScreen.SetActive(true);
                dealScreen.SetActive(false);
                eventTitle.text = eventData[0];
                eventCategory.text = eventData[1];
                eventDesc.text = eventData[2];
                eventExpiry.text = eventData[3];
                break;
            case "Event":
                gameScreen.SetActive(true);
                dealScreen.SetActive(false);
                eventTitle.text = eventData[0];
                eventCategory.text = eventData[1];
                eventDesc.text = eventData[2];
                eventExpiry.text = eventData[3];
                break;
        }
    }
}
