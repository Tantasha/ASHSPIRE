using UnityEngine;

public class confirmation : MonoBehaviour
{
    [SerializeField] private confirmationWindow confirmationWindow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openConfirmation("Are you Sure?");
    }

    private void openConfirmation(string message)
    {
        confirmationWindow.gameObject.SetActive(true);
        confirmationWindow.yesButton.onClick.AddListener(yesClicked);
        confirmationWindow.noButton.onClick.AddListener(noClicked);
        confirmationWindow.messageText.text = message;
    }

    private void yesClicked()
    {
        confirmationWindow.gameObject.SetActive(false);
        Debug.Log("Yes clicked");
    }
    private void noClicked()
    {
        confirmationWindow.gameObject.SetActive(false);
        Debug.Log("No clicked");
    }

}
