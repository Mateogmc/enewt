using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    static AudioManager menuMusic;
    static AudioManager gameMusic;
    static int currentLevel;
    static int levelCount = 10;
    static List<int> levelsRemaining = new List<int>();
    public static int multiplayerLevels = 1;
    public static bool singlePlayer;
    public static int fadingTime = 1200;

    void Start()
    {
        Options.InitialSettings();
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioManager>();
        gameMusic = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<AudioManager>();
        menuMusic.PlayMusic();
        ChangeScene("MainMenu");
    }

    public static void PlayMenuMusic()
    {
        gameMusic.PauseMusic();
        menuMusic.PlayMusic();
    }

    public static void PlayGameMusic()
    {
        menuMusic.PauseMusic();
        gameMusic.PlayMusic();
    }

    public static void ChangeScene(string scenePath)
    {
        SceneManager.LoadScene(scenePath);
    }

    public static void LoadSingleplayer()
    {
        singlePlayer = true;
        currentLevel = 1;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public static void LoadSingleplayer(bool next)
    {
        singlePlayer = true;
        if (next)
        {
            currentLevel++;
        }
        if (currentLevel == levelCount)
        {
            SceneManager.LoadScene("Congratulations");
        } else
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
    }

    public static void ClearScene()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(go);
        }
    }

    public static void StartMultiplayer()
    {
        for (int i = 1; i <= multiplayerLevels; i++)
        {
            levelsRemaining.Add(i);
        }
        LoadMultiplayer();
    }

    public static void LoadMultiplayer()
    {
        if (levelsRemaining.Count == 0)
        {
            StartMultiplayer();
        }
        int nextLevel = Random.Range(0, levelsRemaining.Count - 1);
        SceneManager.LoadScene("Multiplayer" + levelsRemaining[nextLevel]);
        levelsRemaining.Remove(nextLevel);
    }

    async public static void CloseGame()
    {
        Application.Quit();
    }
}
