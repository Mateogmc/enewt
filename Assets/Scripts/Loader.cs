using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    static int currentLevel;
    static int levelCount = 10;
    static List<int> levelsRemaining = new List<int>();
    public static int multiplayerLevels = 1;
    public static bool singlePlayer;
    public static int fadingTime = 1200;

    void Start()
    {
        Options.InitialSettings();
        ChangeScene("MainMenu");
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
        if (currentLevel == levelCount + 1)
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
