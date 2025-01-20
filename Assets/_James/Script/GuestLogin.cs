using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour
{
    public Image playerImage;
    public Sprite guestSprite;
    public TextMeshProUGUI guestName;

    public GameObject menuPanel;
    public GameObject loginPanel;
    public void GuestSignIn()
    {
        playerImage.sprite = guestSprite;
        guestName.text = "GUEST";
        
        menuPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
}
