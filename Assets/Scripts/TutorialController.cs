using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TutorialController : MonoBehaviour
{
    public Fader fader;
    bool changing = false;

    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        EnterScene();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !changing)
        {
            changing = true;
            ExitToMenu();
        }
    }

    async void EnterScene()
    {
        Fader fromBlack = Instantiate(fader);
        fromBlack.unFading = true;
        Time.timeScale = 0f;
        await Task.Delay(Loader.fadingTime);
        Time.timeScale = 1f;
    }
    async void ExitToMenu()
    {
        soundManager.PlaySound(soundManager.menuSelect);
        Loader.ClearScene();
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        Loader.ChangeScene("MainMenu");
    }

}
