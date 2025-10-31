using UnityEngine;
using UnityEngine.SceneManagement;

/// Tracks the currently loaded environment scene name and provides events for scene changes.
public class GameSceneManager : MonoBehaviour
{
    // Static instance makes it easy for other scripts (like UIManager) to access
    public static GameSceneManager Instance { get; private set; }

    [Tooltip("The name of the currently active environment scene.")]
    public string CurrentEnvironmentSceneName { get; private set; } = "Default";

    // Action event that UIManager will subscribe to.
    public event System.Action<string> OnEnvironmentSceneChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep this manager across scene loads

        // Register for the scene loaded event when the game starts
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Clean up when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // Update the current environment name
        CurrentEnvironmentSceneName = scene.name;

        OnEnvironmentSceneChanged?.Invoke(CurrentEnvironmentSceneName);

        Debug.Log($"Environment changed to: {CurrentEnvironmentSceneName}");
    }
}

