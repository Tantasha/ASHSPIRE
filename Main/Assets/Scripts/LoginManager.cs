using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class LoginManager : MonoBehaviour
{
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

    //Once the registeration is successful then there should be a message that show it was successful
    // and open the login page
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Message.text = "Registeration Successful!";
        OpenLogin();
        RegisterEmail.text = "";
        RegisterPassword.text = "";
        RegisterUsername.text = "";
        Message.text = "";

    }

    //If the registeration fails, it should let the player know of the error
    private void OnRegisterFailure(PlayFabError error)
    {
        Message.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
        Message.text = "";
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

    //Successful login results in Loggin in the game
    private void OnLoginSuccess(LoginResult result)
    {
        Message.text = "Loggin in...";
        SceneManager.LoadScene("LoadingScene");

    }

    //When the log in fails, there should be a message, and should let the player reinput their login details
    private void OnLoginFail(PlayFabError error)
    {
        Message.text = " Login Failure... \n Try Again.";
        LoginEmail.text = "";
        LoginPassword.text = "";
        Message.text = "";
    }

    //When player forgets their password. 
    //Player should be able to reset their password.
    public void Recovery()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = ForgotEmail.text,
            TitleId = "F32A3",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnRecoveryFail);
    }

    //Once the player inputs existing email address
    //the player should receive a recovery email to reset their password
    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        OpenLogin();
        Message.text = "Recovery Email Sent.";
        ForgotEmail.text = "";


    }

    //If the player input an invalid email address
    private void OnRecoveryFail(PlayFabError error)
    {
        Message.text = "Invalid Email Address";
        Message.text = "";
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


}
