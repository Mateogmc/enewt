using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        source.volume = Options.musicVolume;
    }

    public void PlayMusic()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    public void PauseMusic()
    {
        source.Stop();
    }
}
