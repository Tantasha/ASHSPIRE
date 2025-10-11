using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ReturnMain()
    {
        SceneManager.LoadSceneAsync(0);//changes the scene to the main menu
    }
}
