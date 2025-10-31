using UnityEngine;
using UnityEngine.SceneManagement;


public class VolcanoScene : MonoBehaviour
{
    public string sceneToLoad = "Volcano";

    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
