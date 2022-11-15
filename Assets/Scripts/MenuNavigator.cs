using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

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
        Fader toBlack = Instantiate(fader);
        toBlack.fading = true;
        changingScene = true;
        await Task.Delay(1000);
        switch (selection)
        {
            case 0:
                Loader.StartSingleplayer();
                break;

            case 1:

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
        Instantiate(fader);
        await Task.Delay(1000);
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
            selection++;
            if (selection > 3)
            {
                selection = 0;
            }
            movedUp = true;
        }
        else if (Input.GetKeyDown((KeyCode)controls.keyboard.forward) || (Input.GetAxis("Vertical1") < -0.9 && !movedDown))
        {
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
