using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public string sceneToLoad = "Map";

    public void LoadMap()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
