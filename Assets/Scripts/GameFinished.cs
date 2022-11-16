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

    async void ExitToMenu()
    {
        Loader.ClearScene();
        Instantiate(fader);
        await Task.Delay(Loader.fadingTime);
        Loader.ChangeScene("MainMenu");
    }
}
