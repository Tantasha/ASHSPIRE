using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public string sceneToLoad = "Map";
    public float delayBeforeLoad = 0.5f; // Give time for UI to render

    private float timer = 0f;
    private bool hasStartedLoading = false;

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (!hasStartedLoading && timer >= delayBeforeLoad)
        {
            hasStartedLoading = true;
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
