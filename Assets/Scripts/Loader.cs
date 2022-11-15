using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Loader : MonoBehaviour
{
    AudioManager audioManager;
    public static int currentLevel;
    void Start()
    {
        Options.InitialSettings();
        audioManager = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioManager>();
        audioManager.PlayMusic();
        ChangeScene("MainMenu");
    }

    public static void ChangeScene(string scenePath)
    {
        SceneManager.LoadScene(scenePath);
    }

    public static void StartSingleplayer()
    {
        SceneManager.LoadScene("Level1");
    }

    public static void StartMultiplayer()
    {

    }

    async public static void CloseGame()
    {
        Application.Quit();
    }
}
