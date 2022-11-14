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


    void Start()
    {
        selection = 0;
        reader = new JSONReader();
        controls = reader.GetControls(1);
        singleplayerRenderer = singleplayer.GetComponent<SpriteRenderer>();
        multiplayerRenderer = multiplayer.GetComponent<SpriteRenderer>();
        optionsRenderer = options.GetComponent<SpriteRenderer>();
        exitRenderer = exit.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SelectionManagement();
        InputDetection();
    }

    async void OptionSelected()
    {
        switch (selection)
        {
            case 0:
                Instantiate(fader);
                break;

            case 1:
                Instantiate(fader);
                break;

            case 2:
                Fader toBlack = Instantiate(fader);
                toBlack.fading = true;
                await Task.Delay(1000);
                SceneManager.LoadScene("Level1");
                break;

            case 3:
                Instantiate(fader);
                break;
        }
    }

     void InputDetection()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
