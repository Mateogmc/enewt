using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    public AudioClip menuSelect;
    public AudioClip menuNavigation;
    public AudioClip menuSlider;
    public AudioClip bulletRebound;
    public AudioClip bulletBreak;
    public AudioClip cannonFire;
    public AudioClip levelLost;
    public AudioClip levelWon;
    public AudioClip tankDestroyed;
    public AudioClip exitLevel;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip, Options.soundVolume);
    }
}
