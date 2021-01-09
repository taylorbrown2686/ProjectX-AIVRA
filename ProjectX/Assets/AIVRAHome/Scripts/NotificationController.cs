using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public string notificationType, sender, databaseID;
    [SerializeField] private Text senderText, messageText;
    [SerializeField] private GameObject[] buttonContainers;

    private void OnEnable()
    {
        foreach (GameObject obj in buttonContainers)
        {
            obj.SetActive(false);
        }
    }
    public void NotificationClicked()
    {
        OnEnable();
        switch (notificationType)
        {
            case "FriendReq":
                buttonContainers[0].SetActive(true);
                break;
            case "FriendAccept":
                //nothing yet
                break;
            case "UpdateAvailable":
                buttonContainers[1].SetActive(true);
                break;
            case "SubExpiring":
                buttonContainers[2].SetActive(true);
                break;
            case "NoOptions":
                buttonContainers[3].SetActive(true);
                break;
        }
    }

    public void PopulateNotification(string sender, string timestamp, string message)
    {
        senderText.text = "From: " + sender + " on " + timestamp;
        messageText.text = message;
    }
    //
    //DATABASE CALLS
    //

    private IEnumerator RemoveNotification()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", databaseID);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/removeNotification.php", form);
        yield return www;
        Destroy(this.gameObject);
    }

    private IEnumerator AcceptFriend()
    {
        WWWForm form = new WWWForm();
        form.AddField("accepter", CrossSceneVariables.Instance.email);
        form.AddField("sender", sender);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/addFriendAfterConfirmedRequest.php", form);
        yield return www;
        Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
            CrossSceneVariables.Instance.email, sender, "FriendAccept", CrossSceneVariables.Instance.username + " has accepted your friend request!"));
        StartCoroutine(RemoveNotification());
    }

    private IEnumerator DeclineFriend()
    {
        WWWForm form = new WWWForm();
        form.AddField("receiver", CrossSceneVariables.Instance.email);
        form.AddField("sender", sender);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/declineFriendRequest.php", form);
        yield return www;
        print(www.text);
        StartCoroutine(RemoveNotification());
    }

    //
    //BUTTON ONCLICK METHODS
    //
    public void CloseMenu()
    {
        OnEnable();
    }

    public void RemoveNotificationOnClick()
    {
        StartCoroutine(RemoveNotification());
    }
    public void AcceptFriendOnClick()
    {
        StartCoroutine(AcceptFriend());
    }

    public void DeclineFriendOnClick()
    {
        StartCoroutine(DeclineFriend());
    }

    public void GoToUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.aivra.projectxx&hl=en_US&gl=US");
        } else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            print("no ios port yet");
            //Application.OpenURL();
        }
    }
}
