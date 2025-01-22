using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject sharePanel;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI dateText;

    public void ShareScreenShot()
    {
        winText.text = GetComponent<GameManager>().GetWinText(); //show who win
        DateTime dateTime = DateTime.Now;
        dateText.text = string.Format("{0}/{1}/{2}",dateTime.Day,dateTime.Month,dateTime.Year);
        gameOverPanel.SetActive(false);
        sharePanel.SetActive(true);
        StartCoroutine(TakeScreenShot());
    }

    IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();
        
        string path = Path.Combine(Application.temporaryCachePath,"sharedImage.png");
        File.WriteAllBytes(path, texture2D.EncodeToPNG());
        
        Destroy(texture2D);
        
        new NativeShare().AddFile(path).SetSubject("This is my win").SetText("Share screen to someone").Share();
        sharePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
