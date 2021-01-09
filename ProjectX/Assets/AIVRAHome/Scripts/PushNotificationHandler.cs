using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class PushNotificationHandler : MonoBehaviour
{
    AndroidNotificationChannel defaultNotificationChannel;
    int identifier;

    private void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Description = "For all notifications",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);

        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Thanks for trying AIVRA Beta!",
            Text = "Let us know how it is at taylorbrown@aivra.com",
            SmallIcon = "icon_small",
            LargeIcon = "icon_large",
            FireTime = System.DateTime.Now.AddSeconds(30),
        };

        identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");

        AndroidNotificationCenter.NotificationReceivedCallback receivedNotificationHandler = delegate (AndroidNotificationIntentData data)
        {
            var msg = "Notification received! " + data.Id + "\n";
        };

        AndroidNotificationCenter.OnNotificationReceived += receivedNotificationHandler;

        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if (notificationIntentData != null)
        {
            Debug.Log("App opened with notification!");
        }
    }
}
