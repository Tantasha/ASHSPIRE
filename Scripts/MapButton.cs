using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour
{
    [SerializeField] private string mapSceneName = "Map";

    public void OnMapButtonClick()
    {
        SceneTracker.CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(mapSceneName);
    }
}
