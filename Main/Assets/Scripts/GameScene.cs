using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public string sceneToLoad = "controls";
    
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
