using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Audio;

public class MenuNavigator : MonoBehaviour
{
    public GameObject singleplayer;
    public GameObject multiplayer;
    public GameObject options;
    public GameObject exit;
    public Fader fader;
    SpriteRenderer singleplayerRenderer;
    SpriteRenderer multiplayerRenderer;
    SpriteRenderer optionsRenderer;
    SpriteRenderer exitRenderer;
    JSONReader reader;
    JSONReader.Player controls;

    int selection;
    bool movedDown = false;
    bool movedUp = false;
    bool changingScene;

    SoundManager soundManager;


    void Start()
    {
        Options.InitialSettings();
        selection = 0;
        reader = new JSONReader();
        controls = reader.GetControls(1);
        singleplayerRenderer = singleplayer.GetComponent<SpriteRenderer>();
        multiplayerRenderer = multiplayer.GetComponent<SpriteRenderer>();
        optionsRenderer = options.GetComponent<SpriteRenderer>();
        exitRenderer = exit.GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        changingScene = true;
        EnterScene();
    }

    private void Update()
    {
        if (!changingScene)
        {
            SelectionManagement();
            InputDetection();
        }
    }

    async void OptionSelected()
    {
        soundManager.PlaySound(soundManager.menuSelect);
        Instantiate(fader);
        changingScene = true;
        await Task.Delay(Loader.fadingTime);
        switch (selection)
        {
            case 0:
                Options.playing = true;
                Loader.singlePlayer = true;
                Loader.LoadSingleplayer();
                break;

            case 1:
                Options.playing = true;
                Loader.singlePlayer = false;
                Loader.LoadMultiplayer();
                break;

            case 2:
                Loader.ChangeScene("Options");
                break;

            case 3:
                Application.Quit();
                break;
        }
    }

    async void EnterScene()
    {
        Fader fromBlack = Instantiate(fader);
        fromBlack.unFading = true;
        await Task.Delay(Loader.fadingTime);
        changingScene = false;
    }

    void InputDetection()
    {
        if (Input.GetKeyDown((KeyCode)controls.keyboard.fire) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown((KeyCode)controls.gamepad.fire))
        {
            OptionSelected();
        }

        if (Input.GetKeyDown((KeyCode)controls.keyboard.backwards) || (Input.GetAxis("Vertical1") > 0.9 && !movedUp))
        {
            soundManager.PlaySound(soundManager.menuNavigation);
            selection++;
            if (selection > 3)
            {
                selection = 0;
            }
            movedUp = true;
        }
        else if (Input.GetKeyDown((KeyCode)controls.keyboard.forward) || (Input.GetAxis("Vertical1") < -0.9 && !movedDown))
        {
            soundManager.PlaySound(soundManager.menuNavigation);
            selection--;
            if (selection < 0)
            {
                selection = 3;
            }
            movedDown = true;
        }
        if (Input.GetAxis("Vertical1") > -0.6)
        {
            movedDown = false;
        }
        if (Input.GetAxis("Vertical1") < 0.6)
        {
            movedUp = false;
        }
    }

    void SelectionManagement()
    {
        switch (selection)
        {
            case 0:
                singleplayerRenderer.enabled = true;
                multiplayerRenderer.enabled = false;
                optionsRenderer.enabled = false;
                exitRenderer.enabled = false;
                break;

            case 1:
                singleplayerRenderer.enabled = false;
                multiplayerRenderer.enabled = true;
                optionsRenderer.enabled = false;
                exitRenderer.enabled = false;
                break;

            case 2:
                singleplayerRenderer.enabled = false;
                multiplayerRenderer.enabled = false;
                optionsRenderer.enabled = true;
                exitRenderer.enabled = false;
                break;

            case 3:
                singleplayerRenderer.enabled = false;
                multiplayerRenderer.enabled = false;
                optionsRenderer.enabled = false;
                exitRenderer.enabled = true;
                break;
        }
    }
}
