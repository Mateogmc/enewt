using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameFinished : MonoBehaviour
{
    public Fader fader;
    public SpriteRenderer title;
    public float upTime;
    bool changing = false;
    float time = 0f;

    public SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        EnterScene();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= upTime)
        {
            time = 0f;
            title.enabled = !title.enabled;
        }
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !changing)
        {
            changing = true;
            ExitToMenu();
        }
    }

    async void EnterScene()
    {
        Options.paused = true;
        Fader fromBlack = Instantiate(fader);
        fromBlack.unFading = true;
        Time.timeScale = 0f;
        await Task.Delay(Loader.fadingTime);
        Time.timeScale = 1f;
        Options.paused = false;
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
}
