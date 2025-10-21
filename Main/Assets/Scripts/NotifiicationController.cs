using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using System.Collections;
using System.Collections.Generic;

public class NotifiicationController : MonoBehaviour
{
    [SerializeField] AndroidNotifications androidNotifications;

    private void Start()
    {
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus == false)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            androidNotifications.SendNotification("ASHSPIRE", "Next event in 2 days", 2);

        }
    }
}
