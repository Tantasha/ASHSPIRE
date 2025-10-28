using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertScene : MonoBehaviour
{
    public string sceneToLoad = "Desert";

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
