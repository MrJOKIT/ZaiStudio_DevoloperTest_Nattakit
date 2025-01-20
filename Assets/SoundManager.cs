using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    
}
[Serializable]
public class SoundData
{
    public SoundName soundName;
    public AudioClip audioClip;
    [Range(0,1)] public float volume;
    public bool loop;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public List<SoundData> soundData;

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
        
        audioSource.loop = sound.loop;
        audioSource.volume = sound.volume;
        audioSource.clip = sound.audioClip;
        audioSource.Play();
    }
}
