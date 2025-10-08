using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LocationHistoryTracker LocationHistoryTracker { get; private set; }
    public DialogueHistoryTracker DialogueHistoryTracker { get; private set; }
    public QuestManager QuestManager { get; private set; }

    private void Awake()
    {
        // Singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize references (assumes these components are on the same GameObject)
        LocationHistoryTracker = GetComponent<LocationHistoryTracker>();
        DialogueHistoryTracker = GetComponent<DialogueHistoryTracker>();
        QuestManager = GetComponent<QuestManager>();

        // Optional: Warn if components are missing
        if (LocationHistoryTracker == null)
            Debug.LogWarning("Missing LocationHistoryTracker on GameManager.");
        if (DialogueHistoryTracker == null)
            Debug.LogWarning("Missing DialogueHistoryTracker on GameManager.");
        if (QuestManager == null)
            Debug.LogWarning("Missing QuestManager on GameManager.");
    }
}
