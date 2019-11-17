using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using PlayFab.ClientModels;
using PlayFab;

public class Authenticate : MonoBehaviour
{
    public static UserAccountInfo accountInfo;

    public GameObject UpdateDisplayName;
    public GameObject Loading;
    public GameObject Home;
    public GameObject Splash;

    public GameObject YesOrNoDialog;

    // used for device ID
    public static string android_id = string.Empty; // device ID to use with PlayFab login
    public static string ios_id = string.Empty; // device ID to use with PlayFab login
    public static string custom_id = string.Empty; // custom id for other platforms


    /* Communication is diseminated across these 4 events */
    //called after a successful login 
    //public delegate void SuccessfulLoginHandler(string details, MessageDisplayStyle style);
    //public static event SuccessfulLoginHandler OnLoginSuccess;

    //called after a login error or when logging out
    //public delegate void FailedLoginHandler(string details, MessageDisplayStyle style);
    //public static event FailedLoginHandler OnLoginFail;


    private void Start()
    {
    }


    void SigninWithDeviceID(bool createAccount = true)
    {
        UnityAction accountNotFoundCallback = EnableUserSelectMode;

        Loading.SetActive(true);
        LoginWithDeviceId(createAccount, accountNotFoundCallback);
    }

    public void OnClickedYesButton()
    {
        SigninWithDeviceID();
    }

    public void OnClickedNoButton()
    {
        YesOrNoDialog.SetActive(false);
    }

    public void OnClickedPressButton()
    {
        YesOrNoDialog.SetActive(true);
    }

    /// <summary>
    /// Logins the with device identifier (iOS & Android only).
    /// </summary>
    public void LoginWithDeviceId(bool createAcount, UnityAction errCallback)
    {
        Action<bool> processResponse = (bool response) =>
        {
            if (response && GetDeviceId())
            {
                if (!string.IsNullOrEmpty(android_id))
                {
                    Debug.Log("Using Android Device ID: " + android_id);
                    var request = new LoginWithAndroidDeviceIDRequest
                    {
                        AndroidDeviceId = android_id,
                        TitleId = PlayFabSettings.TitleId,
                        CreateAccount = createAcount
                    };

                    //DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.GenericLogin);
                    PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginResult, (PlayFabError error) =>
                    {
                        if (errCallback != null && error.Error == PlayFabErrorCode.AccountNotFound)
                        {
                            errCallback();
                            //PF_Bridge.RaiseCallbackError("Account not found, please select a login method to continue.", PlayFabAPIMethods.GenericLogin, MessageDisplayStyle.none);
                        }
                        else
                        {
                            OnLoginError(error);
                        }

                    });
                }
                else if (!string.IsNullOrEmpty(ios_id))
                {
                    Debug.Log("Using IOS Device ID: " + ios_id);
                    var request = new LoginWithIOSDeviceIDRequest
                    {
                        DeviceId = ios_id,
                        TitleId = PlayFabSettings.TitleId,
                        CreateAccount = createAcount
                    };

                    //DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.GenericLogin);
                    PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginResult, (PlayFabError error) =>
                    {
                        if (errCallback != null && error.Error == PlayFabErrorCode.AccountNotFound)
                        {
                            errCallback();
                            //PF_Bridge.RaiseCallbackError("Account not found, please select a login method to continue.", PlayFabAPIMethods.GenericLogin, MessageDisplayStyle.none);
                        }
                        else
                        {
                            OnLoginError(error);
                        }
                    });
                }
            }
            else
            {
                Debug.Log("Using custom device ID: " + custom_id);
                var request = new LoginWithCustomIDRequest
                {
                    CustomId = custom_id,
                    TitleId = PlayFabSettings.TitleId,
                    CreateAccount = createAcount
                };

                //DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.GenericLogin);
                PlayFabClientAPI.LoginWithCustomID(request, OnLoginResult, error =>
                {
                    if (errCallback != null && error.Error == PlayFabErrorCode.AccountNotFound)
                    {
                        errCallback();
                        //PF_Bridge.RaiseCallbackError("Account not found, please select a login method to continue.", PlayFabAPIMethods.GenericLogin, MessageDisplayStyle.none);
                    }
                    else
                    {
                        OnLoginError(error);
                    }
                });
            }
        };

        processResponse(true);
        //DialogCanvasController.RequestConfirmationPrompt("Login With Device ID", "Logging in with device ID has some issue. Are you sure you want to contine?", processResponse);
    }

    /// <summary>
    /// Called on a successful login attempt
    /// </summary>
    /// <param name="result">Result object returned from PlayFab server</param>
    private void OnLoginResult(PlayFab.ClientModels.LoginResult result) //LoginResult
    {
        //PF_PlayerData.PlayerId = result.PlayFabId;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.isEditor)
        {

            //if (usedManualFacebook)
            //{
            //    LinkDeviceId();
            //    usedManualFacebook = false;
            //}
        }
        Debug.Log("OnLoginResult");
        GetUserAccountInfo();

        //PF_Bridge.RaiseCallbackSuccess(string.Empty, PlayFabAPIMethods.GenericLogin, MessageDisplayStyle.none);
        //PF_PubSub.currentEntity = new PlayFab.Sockets.Models.EntityKey() {
        //    Type = result.EntityToken.Entity.Type,
        //    Id = result.EntityToken.Entity.Id
        //};
        //PF_PubSub.InitializePubSub();
        //if (OnLoginSuccess != null)
        //    OnLoginSuccess(string.Format("SUCCESS: {0}", result.SessionTicket), MessageDisplayStyle.error);
    }

    public void GetUserAccountInfo()
    {
        var request = new GetPlayerCombinedInfoRequest
        {
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetUserData = true, GetUserReadOnlyData = true, GetUserInventory = true, GetUserVirtualCurrency = true, GetUserAccountInfo = true, GetPlayerStatistics = true }
        };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGetUserAccountInfoSuccess, PlayFabErrorCallback);
    }

    private void OnGetUserAccountInfoSuccess(GetPlayerCombinedInfoResult result)
    {
        accountInfo = result.InfoResultPayload.AccountInfo;

        Debug.Log("Username: " + accountInfo.TitleInfo.DisplayName);
        if (accountInfo.TitleInfo.DisplayName != string.Empty && accountInfo.TitleInfo.DisplayName != null)
        {
            Loading.SetActive(false);
            Splash.SetActive(true);
        }
        else
        {
            Loading.SetActive(false);
            UpdateDisplayName.SetActive(true);
        }

    }

    public void PlayFabErrorCallback(PlayFab.PlayFabError error)
    {
        Debug.Log("PlayFabErrorCallback: " + error.ErrorMessage);
        Loading.SetActive(false);
        UpdateDisplayName.SetActive(true);
    }

    /// <summary>
    /// Raises the login error event.
    /// </summary>
    /// <param name="error">Error.</param>
    private void OnLoginError(PlayFabError error) //PlayFabError
    {
        string errorMessage;
        if (error.Error == PlayFabErrorCode.InvalidParams && error.ErrorDetails.ContainsKey("Password"))
            errorMessage = "Invalid Password";
        else if (error.Error == PlayFabErrorCode.InvalidParams && error.ErrorDetails.ContainsKey("Username") || (error.Error == PlayFabErrorCode.InvalidUsername))
            errorMessage = "Invalid Username";
        else if (error.Error == PlayFabErrorCode.AccountNotFound)
            errorMessage = "Account Not Found, you must have a linked PlayFab account. Start by registering a new account or using your device id";
        else if (error.Error == PlayFabErrorCode.AccountBanned)
            errorMessage = "Account Banned";
        else if (error.Error == PlayFabErrorCode.InvalidUsernameOrPassword)
            errorMessage = "Invalid Username or Password";
        else
            errorMessage = string.Format("Error {0}: {1}", error.HttpCode, error.ErrorMessage);

        //if (OnLoginFail != null)
        //{
        //    OnLoginFail(errorMessage, MessageDisplayStyle.error);
        //}

        Loading.SetActive(false);
        Debug.Log("OnLoginError: " + errorMessage);

        // reset these IDs (a hack for properly detecting if a device is claimed or not, we will have an API call for this soon)
        //PlayFabLoginCalls.android_id = string.Empty;
        //PlayFabLoginCalls.ios_id = string.Empty;

    }


    /// <summary>
    /// Gets the device identifier and updates the static variables
    /// </summary>
    /// <returns><c>true</c>, if device identifier was obtained, <c>false</c> otherwise.</returns>
    public static bool GetDeviceId(bool silent = false) // silent suppresses the error
    {
        if (CheckForSupportedMobilePlatform())
        {
#if UNITY_ANDROID
            //http://answers.unity3d.com/questions/430630/how-can-i-get-android-id-.html
            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
#endif

#if UNITY_IPHONE
			ios_id = UnityEngine.iOS.Device.vendorIdentifier;
#endif
            return true;
        }
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
            return false;
        }
    }

    /// <summary>
    /// Check to see if our current platform is supported (iOS & Android)
    /// </summary>
    /// <returns><c>true</c>, for supported mobile platform, <c>false</c> otherwise.</returns>
    public static bool CheckForSupportedMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }








    public void EnableUserSelectMode()
    {

    }
}
