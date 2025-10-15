using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestScene : MonoBehaviour
{
    public string sceneToLoad = "controls";
    
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
