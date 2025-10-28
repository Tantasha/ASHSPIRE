using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowScene : MonoBehaviour
{
    public string sceneToLoad = "Snow";

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
