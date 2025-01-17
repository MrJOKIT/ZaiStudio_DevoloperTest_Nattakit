using System;
using System.Collections;
using System.Collections.Generic;
using Google;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class GoogleAuthentication : MonoBehaviour
{
    [Header("Google ID")]
    public string imageURL;
    public string webClientID = "997092099467-8fib0dvlmioojgsnha62js1br9n8237j.apps.googleusercontent.com";
    
    [Header("Google UI")]
    public TextMeshProUGUI userNameText;
    public Image profileImage;

    [Header("UI Panel")] 
    public GameObject loginPanel; 
    public GameObject profilePanel;
    
    [Header("Guest ID")]
    public string guestName;
    public Sprite guestProfileImage;
    
    private GoogleSignInConfiguration configuration;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientID,
            RequestIdToken = true,
            UseGameSignIn = false,
            RequestEmail = true
        };
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished, TaskScheduler.Default);
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("Got Error: " + error.Status + ": " + error.Message);
                }
                else
                {
                    Debug.LogError("Got unexpected exception?: " + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Canceled");
        }
        else
        {
            StartCoroutine(UpdateUI(task.Result));
        }
    }

    IEnumerator UpdateUI(GoogleSignInUser user)
    {
        Debug.Log(user.DisplayName +" Sign In");
        
        userNameText.text = user.DisplayName;
        imageURL = user.ImageUrl.ToString();
        
        loginPanel.SetActive(false);
        yield return null;
        profilePanel.SetActive(true);
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();
        Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
        Rect rect = new Rect(0, 0, downloadedTexture.width, downloadedTexture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        profileImage.sprite = Sprite.Create(downloadedTexture, rect, pivot);
    }
    
    IEnumerator GuestUpdateUI()
    {
        Debug.Log("Guest Sign In");
        
        userNameText.text = "GUEST";
        profileImage.sprite = guestProfileImage;
        loginPanel.SetActive(false);
        yield return null;
        profilePanel.SetActive(true);
        
        
    }

    public void OnSignOut() 
    {
        userNameText.text = "";
        
        imageURL = "";
        loginPanel.SetActive(true);
        profilePanel.SetActive(false);
        
        Debug.Log("Sign Out");
        GoogleSignIn.DefaultInstance.SignOut();
    }
}
