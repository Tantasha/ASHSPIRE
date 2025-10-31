using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ReturnMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
