using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void ReturnMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
