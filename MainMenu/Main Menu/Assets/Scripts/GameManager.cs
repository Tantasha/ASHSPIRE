using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Persistent Objects (optional, set in Inspector)")]
    public GameObject[] persistentObjects;

    // Exposed trackers so other systems can use GameManager.Instance.X
    public DialogueHistoryTracker DialogueHistoryTracker { get; private set; }
    public LocationHistoryTracker LocationHistoryTracker { get; private set; }
    public QuestManager QuestManager { get; private set; }

    private void Awake()
    {
        // singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure objects listed in inspector persist
        MarkPersistentObjects();

        // Try to find trackers/managers if they're in the scene
        // (These will find the first active instance in the scene)
        DialogueHistoryTracker = FindObjectOfType<DialogueHistoryTracker>();
        LocationHistoryTracker = FindObjectOfType<LocationHistoryTracker>();
        QuestManager = FindObjectOfType<QuestManager>();

        // Optional warnings
        if (DialogueHistoryTracker == null)
            Debug.LogWarning("GameManager: DialogueHistoryTracker not found in scene.");
        if (LocationHistoryTracker == null)
            Debug.LogWarning("GameManager: LocationHistoryTracker not found in scene.");
        if (QuestManager == null)
            Debug.LogWarning("GameManager: QuestManager not found in scene.");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // When a new scene loads, re-find scene-local objects if needed
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (DialogueHistoryTracker == null)
            DialogueHistoryTracker = FindObjectOfType<DialogueHistoryTracker>();
        if (LocationHistoryTracker == null)
            LocationHistoryTracker = FindObjectOfType<LocationHistoryTracker>();
        if (QuestManager == null)
            QuestManager = FindObjectOfType<QuestManager>();
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                DontDestroyOnLoad(obj);
        }
    }
}
