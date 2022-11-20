using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader
{
    public string path = "controls.config";

    [System.Serializable]
    public class Controls
    {
        public Player player1 = new Player();
        public Player player2 = new Player();
    }

    [System.Serializable]
    public class Player
    {
        public int id;
        public bool gamepadConnected;
        public Keyboard keyboard = new Keyboard();
        public Gamepad gamepad = new Gamepad();
    }

    [System.Serializable]
    public class Keyboard
    {
        public int rotLeft;
        public int rotRight;
        public int aimLeft;
        public int aimRight;
        public int fire;
        public int forward;
        public int backwards;
    }

    [System.Serializable]
    public class Gamepad
    {
        public string aim;
        public int fire;
        public string horizontal;
        public string forward;
        public string backwards;
    }

    public Player GetControls(int id)
    {
        Controls control = new Controls();
        using (StreamReader reader = new StreamReader(path))
        {
            control = JsonUtility.FromJson<Controls>(reader.ReadToEnd());
        }
        return id == 1 ? control.player1 : control.player2;
    }

    void Start()
    {
        InitializeControls();
    }

    public void InitializeControls()
    {
        Controls controls = new Controls();
        controls.player1.id = 1;
        controls.player2.id = 2;
        controls.player1.gamepadConnected = true;
        controls.player2.gamepadConnected = true;

        controls.player1.keyboard.rotLeft = (int)KeyCode.LeftArrow;
        controls.player1.keyboard.rotRight = (int)KeyCode.RightArrow;
        controls.player1.keyboard.aimLeft = (int)KeyCode.J;
        controls.player1.keyboard.aimRight = (int)KeyCode.L;
        controls.player1.keyboard.fire = (int)KeyCode.K;
        controls.player1.keyboard.forward = (int)KeyCode.UpArrow;
        controls.player1.keyboard.backwards = (int)KeyCode.DownArrow;

        controls.player2.keyboard.rotLeft = (int)KeyCode.A;
        controls.player2.keyboard.rotRight = (int)KeyCode.D;
        controls.player2.keyboard.aimLeft = (int)KeyCode.C;
        controls.player2.keyboard.aimRight = (int)KeyCode.B;
        controls.player2.keyboard.fire = (int)KeyCode.V;
        controls.player2.keyboard.forward = (int)KeyCode.W;
        controls.player2.keyboard.backwards = (int)KeyCode.S;

        controls.player1.gamepad.aim = "Aim1";
        controls.player1.gamepad.fire = (int)KeyCode.Joystick1Button5;
        controls.player1.gamepad.horizontal = "Horizontal1";
        controls.player1.gamepad.forward = "Forward1";
        controls.player1.gamepad.backwards = "Backwards1";

        controls.player2.gamepad.aim = "Aim2";
        controls.player2.gamepad.fire = (int)KeyCode.Joystick2Button5;
        controls.player2.gamepad.horizontal = "Horizontal2";
        controls.player2.gamepad.forward = "Forward2";
        controls.player2.gamepad.backwards = "Backwards2";

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(JsonUtility.ToJson(controls));
        }
    }
}
