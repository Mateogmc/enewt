using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource gameMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Options.playing)
        {
            gameMusic.volume = (Options.paused ? Options.musicVolume/2 : Options.musicVolume);
            menuMusic.volume = 0f;
        } else
        {
            menuMusic.volume = Options.musicVolume;
            gameMusic.volume = 0f;
        }
    }
}
