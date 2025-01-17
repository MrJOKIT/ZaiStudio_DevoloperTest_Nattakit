using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [Header("Login UI")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Image playerImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private LogInController loginController;
    [Space(20)]
    [Header("Player Info")]
    [SerializeField] private GameObject playerInfoPanel;
    [SerializeField] private GameObject loginPanel;

    private void OnEnable()
    {
        loginButton.onClick.AddListener(LogInButtonPressed);
        loginController.OnSignedIn += LogInController_OnSignedIn;
    }

    private void LogInController_OnSignedIn(PlayerInfo playerInfo, string playerName)
    {
        loginPanel.SetActive(false);
        playerInfoPanel.SetActive(true);
        
        //Import google info
        
        playerNameText.text = playerName;
        usernameText.text = playerInfo.Id;
        
        
        Debug.Log("Login Success");
    }

    private async void LogInButtonPressed()
    {
        loginController.InitSignIn();
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(LogInButtonPressed);
        loginController.OnSignedIn -= LogInController_OnSignedIn;
    }
}
