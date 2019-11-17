using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class UIUpdateDisplayName : MonoBehaviour
{
    //public Text displayName;
    public InputField inputField;

    public GameObject Loading;
    public GameObject Home;
    public GameObject Splash;

    static string DisplayName;

    public void SetDisplayName()
    {
        Loading.SetActive(true);
        UpdateDisplayName(inputField.text, result =>
        {
            //displayName.text = result.DisplayName;
            DisplayName = result.DisplayName;
            Authenticate.accountInfo.TitleInfo.DisplayName = result.DisplayName;
        });
    }


    public void UpdateDisplayName(string displayName, UnityAction<UpdateUserTitleDisplayNameResult> callback = null)
    {
        if (displayName.Length < 3 || 20 < displayName.Length)
            return;

        //DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.UpdateDisplayName);
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, result =>
        {
            Loading.SetActive(false);
            Home.SetActive(true);
            Debug.Log("RaiseCallbackSuccess: " + result.DisplayName);
        }, PlayFabErrorCallback);

    }


    public void PlayFabErrorCallback(PlayFab.PlayFabError error)
    {
        Loading.SetActive(false);
        Debug.Log("PlayFabErrorCallback: " + error.ErrorMessage);
    }
}