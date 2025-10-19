using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using System;

public class AndroidNotifications : MonoBehaviour
{
    private bool hasSentNotification = false; //  Prevent repeats in one session

    private void Start()
    {
        RequestAuthorization();
        RegisterNotificationChannel();
    }

    private void OnApplicationFocus(bool focus)
    {
        // Only send once, when the app first loses focus
        if (!focus && !hasSentNotification)
        {
            SendNotification("ASHSPIRE", "Halloween special treats begin on 31st October 🦇🍭", 2);
            hasSentNotification = true; //  Mark as sent for this session
        }
    }

    private void RequestAuthorization()
    {
        // Ask permission for Android 13+
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    private void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.High, //  Ensure visible banner
            Description = "ASHSPIRE Notifications"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendNotification(string title, string text, int fireTimeInSeconds)
    {
        AndroidNotificationCenter.CancelAllNotifications(); // Clear any pending ones

        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            FireTime = DateTime.Now.AddSeconds(2),
            SmallIcon = "icon_0",
            LargeIcon = "icon_1"
        };

        int id = AndroidNotificationCenter.SendNotification(notification, "default_channel");
        Debug.Log($" Notification scheduled once with ID: {id}");
    }
}
