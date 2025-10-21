using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class LoginManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text successText;
    

    /// <summary>
    /// This method is called when the login button is clicked.
    /// </summary>

    public void onLoginButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        // Here you would typically send the email and password to your server for authentication.
        Debug.Log($"Attempting to log in with Email: {email} and Password: {password}");

        string loginResult = checkLoginInfo(email, password);

        if(string.IsNullOrEmpty(loginResult))
        {
            Debug.Log("Login successful!");
            // Proceed to the next scene or main menu
            successText.text = "Login successful!";

        } else
        {
            Debug.LogError($"Login failed: {loginResult}");
            // Display error message to the user
            errorText.text = loginResult;
        }
    }

    /// <summary>
    /// This method checks the login information.
    /// </summary>
    /// <returns>This will return either a successful login or a message reading an error. </returns>
    private string checkLoginInfo(string email, string password)
    {
        string message = "";

        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
        {
            message = "Email and Password cannot be empty.";
        }
        else if (string.IsNullOrEmpty(email))
        {
            message = "Email cannot be empty.";
        }
        else if (string.IsNullOrEmpty(password))
        {
            message = "Password cannot be empty.";
        }
        else if (!email.Contains("@"))
        {
            message = "Email format is invalid.";
        }
        else if (password.Length < 6)
        {
            message = "Password must be at least 6 characters long.";
        }
        Debug.Log(message);
        return message;

    }

    /// <summary>
    /// On any input field value change, this method is called to remove the error message.
    /// 
    public void removeErrorMessage()
    {
        errorText.text = "";
    }
}
