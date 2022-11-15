using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource menuMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        menuMusic = GetComponent<AudioSource>();
    }

    private void Update()
    {
        menuMusic.volume = Options.musicVolume;
    }

    public void PlayMusic()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
    }

    public void PauseMusic()
    {
        menuMusic.Stop();
    }
}
