using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.UI;

public class GooglePlayGameLoginManger : MonoBehaviour
{
    public TextMeshProUGUI detailText;
    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI idText;
    public Image profileImage;
    [Header("Panel")]
    public GameObject loginPanel;
    public GameObject menuPanel;
    public GameObject profilePanel;
    private void Start()
    {
        //SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            
            nameText.text = name;
            idText.text = "ID: " + id;
            
            GameModeManager.instance.InitializePlayerInfo(name, id);
            
            loginPanel.SetActive(false);
            menuPanel.SetActive(true);
            profilePanel.SetActive(true);

            StartCoroutine(LoadProfileImage(imgUrl));
            
            detailText.text = "Sign In Success";
        }
        else
        {
            Debug.Log("SignIn Error");
            detailText.text = "Sign In Failed";
        }
    }

    IEnumerator LoadProfileImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        profileImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
    }
}
