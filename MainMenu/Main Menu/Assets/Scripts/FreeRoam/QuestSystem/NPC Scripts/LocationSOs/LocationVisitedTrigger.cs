using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVisitedTrigger : MonoBehaviour
{
    [SerializeField] private LocationSO locationVisited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LocationHistoryTracker.Instance.RecordLocation(locationVisited);
            Destroy(gameObject);
        }
    }
}
