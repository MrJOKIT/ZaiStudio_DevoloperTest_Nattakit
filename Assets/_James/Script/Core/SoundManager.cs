using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    ClickUI,
    PlayerThrow,
    EnemyThrow,
    Hit,
}

public enum BGMName
{
    BGM1,
    BGM2,
    BGM3,
}
[Serializable]
public class SoundData
{
    public SoundName soundName;
    public AudioClip audioClip;
    [Range(0,1)] public float volume;
}
[Serializable]
public class BGMData
{
    public BGMName bgmName;
    public AudioClip audioClip;
    [Range(0,1)] public float volume;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public AudioSource bgmSource;
    public List<SoundData> soundData;
    public List<BGMData> bgmData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundName name)
    {
        var sound = soundData.Find(x => x.soundName == name);
        
        audioSource.volume = sound.volume;
        audioSource.clip = sound.audioClip;
        audioSource.Play();
    }

    public void PlayBGM(BGMName name)
    {
        var bgm = bgmData.Find(x => x.bgmName == name);
        
        bgmSource.clip = bgm.audioClip;
        bgmSource.volume = bgm.volume;
        bgmSource.Play();
    }
}
