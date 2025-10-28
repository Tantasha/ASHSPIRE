using UnityEngine;
using UnityEngine.SceneManagement;

public class MapClose : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        if (!string.IsNullOrEmpty(SceneTracker.CurrentSceneName))
        {
            SceneManager.LoadScene(SceneTracker.CurrentSceneName);
        }
        else
        {
            Debug.LogWarning("CurrentSceneName is not set. Cannot return to the previous scene.");
        }
    }
}
