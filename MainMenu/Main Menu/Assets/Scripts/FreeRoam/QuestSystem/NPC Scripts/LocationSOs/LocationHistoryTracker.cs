using System.Collections;
using System.Collections.Generic;   // <- fixed
using UnityEngine;

public class LocationHistoryTracker : MonoBehaviour
{
    public static LocationHistoryTracker Instance;

    private readonly HashSet<LocationSO> locationsVisited = new HashSet<LocationSO>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Optional: persist across scenes
        // DontDestroyOnLoad(gameObject);
    }

    public void RecordLocation(LocationSO locationSO)
    {
        if(locationsVisited.Add(locationSO))
        {
            Debug.Log("Visited: " + locationSO.displayName);
        }
    }

    public bool HasVisited(LocationSO locationSO)
    {
        return locationsVisited.Contains(locationSO);  // <- fixed
    }
}

