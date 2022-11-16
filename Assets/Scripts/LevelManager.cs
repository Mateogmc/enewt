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

    void Start()
    {
        singleplayer = Loader.singlePlayer;
        EnterScene();
    }

    void Update()
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
            }
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
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        Loader.LoadSingleplayer(false);
    }

    async void LevelClear()
    {
        Loader.ClearScene();
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
