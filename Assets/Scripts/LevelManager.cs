using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public Fader fader;
    bool singleplayer;
    bool running = false;
    int lastNumberOfEnemies;

    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        singleplayer = Loader.singlePlayer;
        EnterScene();
    }

    void Update()
    {
        if (!Options.paused)
        {
        if (singleplayer)
        {
            int numberOfEnemies = 0;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Tank"))
            {
                numberOfEnemies++;
            }
            if (GameObject.FindGameObjectWithTag("Player") == null && running)
            {
                running = false;
                LevelFailed();
            }
            if (numberOfEnemies == 0 && running)
            {
                running = false;
                LevelClear();
            } else if (numberOfEnemies < lastNumberOfEnemies)
            {
                soundManager.PlaySound(soundManager.tankDestroyed);
            }
            lastNumberOfEnemies = numberOfEnemies;
        } else
        {
            int numberOfPlayers = 0;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                numberOfPlayers++;
            }
            if (numberOfPlayers <= 1 && running)
            {
                running = false;
                LevelClear();
            }
        }
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton7)) && running)
            {
                Time.timeScale = 0f;
                Options.paused = true;
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Options.paused = false;
                Time.timeScale = 1f;
            } else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                Time.timeScale = 1f;
                ExitToMenu();
            }
        }
    }

    async void ExitToMenu()
    {
        soundManager.PlaySound(soundManager.exitLevel);
        Loader.ClearScene();
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        Options.playing = false;
        Loader.ChangeScene("MainMenu");
    }

    async void EnterScene()
    {
        Fader fromBlack = Instantiate(fader);
        fromBlack.unFading = true;
        Time.timeScale = 0f;
        await Task.Delay(Loader.fadingTime);
        Time.timeScale = 1f;
        running = true;
    }

    async void LevelFailed()
    {
        Loader.ClearScene();
        soundManager.PlaySound(soundManager.levelLost);
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        Loader.LoadSingleplayer(false);
    }

    async void LevelClear()
    {
        Loader.ClearScene();
        soundManager.PlaySound(soundManager.levelWon);
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        if (singleplayer)
        {
            Loader.LoadSingleplayer(true);
        } else
        {
            Loader.LoadMultiplayer();
        }
    }
}
