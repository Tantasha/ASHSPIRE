using UnityEngine;
using TMPro;
<<<<<<< HEAD
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System;

=======
>>>>>>> AccountLink


public class LoginManager : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] TextMeshProUGUI Message;
    

    [Header("Login")]
    [SerializeField] TMP_InputField LoginEmail;
    [SerializeField] TMP_InputField LoginPassword;
    [SerializeField] GameObject LoginPanel;


    [Header("Register")]
    [SerializeField] TMP_InputField RegisterUsername;
    [SerializeField] TMP_InputField RegisterEmail;
    [SerializeField] TMP_InputField RegisterPassword;
    [SerializeField] GameObject RegisterPanel;


    [Header("Forgot")]
    [SerializeField] TMP_InputField ForgotEmail;
    [SerializeField] GameObject ForgotPanel;

    void Start()
    {
        PlayFabSettings.staticSettings.TitleId = "F32A3"; // Replace with your actual Title ID
    }



    //Function in opening login page, register page, and forgot page
    #region button functions

    //Register Panel
    public void RegisterUser()
    {
        //message if the password is less than 6 character, null or empty
        if (string.IsNullOrEmpty(RegisterPassword.text) || RegisterPassword.text.Length <= 6)
        {
            Message.text = "Password must be at least 6 characters long.";
        }


        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = RegisterUsername.text,
            Email = RegisterEmail.text,
            Password = RegisterPassword.text,

            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Message.text = "Registeration Successful!";
        OpenLogin();
        RegisterEmail.text = "";
        RegisterPassword.text = "";
        RegisterUsername.text = "";

    }


    private void OnRegisterFailure(PlayFabError error)
    {
        Message.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    //Login panel
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = LoginEmail.text,
            Password = LoginPassword.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFail);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Message.text = "Loggin in...";
        SceneManager.LoadScene("LoadingScene");
    }
    private void OnLoginFail(PlayFabError error)
    {
        Message.text = " Login Failure... \n Try Again.";
        LoginEmail.text = "";
        LoginPassword.text = "";
    }

    public void Recovery()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = ForgotEmail.text,
            TitleId = "F32A3",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnRecoveryFail);
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        OpenLogin();
        Message.text = "Recovery Email Sent.";
        ForgotEmail.text = "";


    }


    private void OnRecoveryFail(PlayFabError error)
    {
        Message.text = "Invalid Email Address";
    }


    public void OpenLogin()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        ForgotPanel.SetActive(false);
    }

    public void OpenRegister()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        ForgotPanel.SetActive(false);
    }

    public void OpenForgot()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(false);
        ForgotPanel.SetActive(true);
    }
    
    #endregion


=======

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
>>>>>>> AccountLink
}
