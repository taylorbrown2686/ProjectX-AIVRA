using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenterController : MonoBehaviour
{
    [SerializeField] private GameObject content, notificationPrefab;
    private void OnEnable()
    {
        StartCoroutine(GetNotifications());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
                "AIVRA Team", CrossSceneVariables.Instance.email, "FriendRequest", "You have a friend request from AIVRA!"));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
                "AIVRA Team", CrossSceneVariables.Instance.email, "UpdateAvailable", "There is an update available for AIVRA!"));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Notifier.Instance.StartCoroutine(Notifier.Instance.SendNotification(
                "AIVRA Team", CrossSceneVariables.Instance.email, "SubExpiring", "Your subscription is about to expire! Renew now!"));
        }
    }

    private IEnumerator GetNotifications()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        WWWForm form = new WWWForm();
        form.AddField("email", CrossSceneVariables.Instance.email);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/getNotificationsFromEmail.php", form);
        yield return www;
        string[] splitString = www.text.Split('#');
        content.GetComponent<LockScrollView>().ChangeItemCount((splitString.Length - 1) / 5);
        for (int i = 0; i < splitString.Length - 1; i += 6)
        {
            GameObject newObj = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity);
            newObj.transform.SetParent(content.transform, false);
            newObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -300 * (i / 5) - 50);
            newObj.GetComponent<NotificationController>().notificationType = splitString[i + 1];
            newObj.GetComponent<NotificationController>().sender = splitString[i];
            newObj.GetComponent<NotificationController>().PopulateNotification(splitString[i], splitString[i + 4], splitString[i + 2]);
            newObj.GetComponent<NotificationController>().databaseID = splitString[i + 5];
        }
    }
}
