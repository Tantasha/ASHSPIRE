using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string storySceneName = "StoryScene";
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";

    // called when story button is clicked
    public void LoadStoryMode()
    {
        SceneManager.LoadScene(storySceneName);
    }
}
