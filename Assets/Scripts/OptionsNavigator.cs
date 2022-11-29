using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Threading.Tasks;

public class OptionsNavigator : MonoBehaviour
{
    public GameObject music;
    public GameObject musicSlider;
    public GameObject musicSliderDeactivated;
    public GameObject sound;
    public GameObject soundSlider;
    public GameObject soundSliderDeactivated;
    public GameObject exit;
    public Fader fader;
    SpriteRenderer musicRenderer;
    SpriteRenderer musicSliderRenderer;
    SpriteRenderer musicSliderDeactivatedRenderer;
    SpriteRenderer soundRenderer;
    SpriteRenderer soundSliderRenderer;
    SpriteRenderer soundSliderDeactivatedRenderer;
    SpriteRenderer exitRenderer;
    JSONReader reader;
    JSONReader.Player controls;

    int selection;
    bool movedDown = false;
    bool movedUp = false;
    bool movedLeft = false;
    bool movedRight = false;
    bool changingScene;

    SoundManager soundManager;

    void Start()
    {
        selection = 0;
        reader = new JSONReader();
        controls = reader.GetControls(1);
        musicRenderer = music.GetComponent<SpriteRenderer>();
        musicSliderRenderer = musicSlider.GetComponent<SpriteRenderer>();
        musicSliderDeactivatedRenderer = musicSliderDeactivated.GetComponent<SpriteRenderer>();
        soundRenderer = sound.GetComponent<SpriteRenderer>();
        soundSliderRenderer = soundSlider.GetComponent<SpriteRenderer>();
        soundSliderDeactivatedRenderer = soundSliderDeactivated.GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();


        exitRenderer = exit.GetComponent<SpriteRenderer>();
        GraphicsManager();
        changingScene = true;
        EnterScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (!changingScene)
        {
            SelectionManagement();
            InputDetection();
        }
    }

    void GraphicsManager()
    {
        musicSlider.transform.position = new Vector2((Options.musicVolume * 5f - 1.25f), musicSlider.transform.position.y);
        musicSliderDeactivated.transform.position = new Vector2((Options.musicVolume * 5f - 1.25f), musicSliderDeactivated.transform.position.y);
        soundSlider.transform.position = new Vector2((Options.soundVolume * 5f - 1.25f), soundSlider.transform.position.y);
        soundSliderDeactivated.transform.position = new Vector2((Options.soundVolume * 5f - 1.25f), soundSliderDeactivated.transform.position.y);
    }

    public void ChangeVolume(bool up)
    {
        if (up)
        {
            if (selection == 0)
            {
                Options.ChangeMusic(0.1f);
            } else if (selection == 1)
            {
                Options.ChangeSound(0.1f);
            }
        } else
        {
            if (selection == 0)
            {
                Options.ChangeMusic(-0.1f);
            }
            else if (selection == 1)
            {
                Options.ChangeSound(-0.1f);
            }
        }
        GraphicsManager();
    }

    async void OptionSelected()
    {
        if (selection == 2)
        {
            soundManager.PlaySound(soundManager.menuSelect);
            Instantiate(fader);
            changingScene = true;
            await Task.Delay(Loader.fadingTime);
            Loader.ChangeScene("MainMenu");
        }
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
            if (selection > 2)
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
                selection = 2;
            }
            movedDown = true;
        }
        if (Input.GetKeyDown((KeyCode)controls.keyboard.rotLeft) || (Input.GetAxis("Horizontal1") < -0.9 && !movedRight))
        {
            ChangeVolume(false);
            soundManager.PlaySound(soundManager.menuSlider);
            movedRight = true;
        }
        else if (Input.GetKeyDown((KeyCode)controls.keyboard.rotRight) || (Input.GetAxis("Horizontal1") > 0.9 && !movedLeft))
        {
            ChangeVolume(true);
            soundManager.PlaySound(soundManager.menuSlider);
            movedLeft = true;
        }
        if (Input.GetAxis("Vertical1") > -0.6)
        {
            movedDown = false;
        }
        if (Input.GetAxis("Vertical1") < 0.6)
        {
            movedUp = false;
        }
        if (Input.GetAxis("Horizontal1") > -0.6)
        {
            movedRight = false;
        }
        if (Input.GetAxis("Horizontal1") < 0.6)
        {
            movedLeft = false;
        }
    }

    void SelectionManagement()
    {
        switch (selection)
        {
            case 0:
                musicRenderer.enabled = true;
                musicSliderRenderer.enabled = true;
                soundRenderer.enabled = false;
                soundSliderRenderer.enabled = false;
                exitRenderer.enabled = false;
                break;

            case 1:
                musicRenderer.enabled = false;
                musicSliderRenderer.enabled = false;
                soundRenderer.enabled = true;
                soundSliderRenderer.enabled = true;
                exitRenderer.enabled = false;
                break;

            case 2:
                musicRenderer.enabled = false;
                musicSliderRenderer.enabled = false;
                soundRenderer.enabled = false;
                soundSliderRenderer.enabled = false;
                exitRenderer.enabled = true;
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
}
