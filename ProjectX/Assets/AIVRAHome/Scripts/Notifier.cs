using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifier : MonoBehaviour
{
    private static Notifier instance = null;

    public static Notifier Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator SendNotification(string sender, string receiver, string notifType, string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("receiver", receiver);
        form.AddField("sender", sender);
        form.AddField("notiftype", notifType);
        form.AddField("message", message);
        WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/sendNotification.php", form);
        yield return www;
    }
}
