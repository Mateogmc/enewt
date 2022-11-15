using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Options
{
    public static float musicVolume = 1f;
    public static float soundVolume = 1f;

    public static void InitialSettings()
    {
        using (StreamReader sr = new StreamReader("Assets/Scripts/options.config"))
        {
            musicVolume = float.Parse(sr.ReadLine());
            soundVolume = float.Parse(sr.ReadLine());
        }
    }

    private static void WriteVolume()
    {
        using (StreamWriter sw = new StreamWriter("Assets/Scripts/options.config"))
        {
            sw.WriteLine(musicVolume.ToString());
            sw.WriteLine(soundVolume.ToString());
        }
    }

    public static void ChangeMusic(float volume)
    {
        musicVolume += volume;
        if (musicVolume > 1f)
        {
            musicVolume = 1f;
        } else if (musicVolume < 0f)
        {
            musicVolume = 0f;
        }
        WriteVolume();
    }

    public static void ChangeSound(float volume)
    {
        soundVolume += volume;
        if (soundVolume > 0.91f)
        {
            soundVolume = 1f;
        }
        else if (soundVolume < 0.09f)
        {
            soundVolume = 0f;
        }
        WriteVolume();
    }
}
