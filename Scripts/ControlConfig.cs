using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlProfile { PC, Controller };

public class ControlConfig : MonoBehaviour
{

    bool isControllerConnected = false;
    public string Controller = "";
    public string PC_Move, PC_Rotate, PC_Item1, PC_Item2, PC_Item3, PC_Item4, PC_Inv, PC_Pause, PC_AttackUse, PC_Aim;
    public string Xbox_Move, Xbox_Rotate, Xbox_Item1, Xbox_Item2, Xbox_Item3, Xbox_Item4, Xbox_Inv, Xbox_Pause, Xbox_AttackUse, Xbox__Aim;
    public ControlProfile cProfile;
    string ControlScheme;
    public KeyCode pcItem1, pcItem2, pcItem3, pcItem4, pcInv, pcPause, pcAttackUse, pcAim, xInv, xPause;

    void DetectController()
    {
        try
        {
            if(Input.GetJoystickNames()[0] != null)
            {
                isControllerConnected =  true;
                cProfile = ControlProfile.Controller;
                IdentifyController();
            }
            else
            {
                cProfile = ControlProfile.PC;
            }
        }
        catch
        {
            isControllerConnected = false;
        }
    }

    void IdentifyController()
    {
        Controller = Input.GetJoystickNames()[0];
    }

    void SwitchProfile (ControlProfile Switcher)
    {
        cProfile = Switcher;
    }

    void SetDefaultValues()
    {
        if(!isControllerConnected)
        {
            PC_Move = "WASD";
            PC_Rotate = "Mouse";
            PC_Item1 = "1";
            PC_Item2 = "2";
            PC_Item3 = "3";
            PC_Item4 = "4";
            PC_Inv = "I";
            PC_Pause = "Escape";
            PC_AttackUse = "Left Mouse Button";
            PC_Aim = "Right Mouse Button";
        }
        else
        {
            PC_Move = "WASD";
            PC_Rotate = "Mouse";
            PC_Item1 = "1";
            PC_Item2 = "2";
            PC_Item3 = "3";
            PC_Item4 = "4";
            PC_Inv = "I";
            PC_Pause = "Escape";
            PC_AttackUse = "Left Mouse Button";
            PC_Aim = "Right Mouse Button";

            Xbox_Move = "Left Thumbstick";
            Xbox_Rotate = "Right Thumbstick";
            Xbox_Item1 = "D-Pad Up";
            Xbox_Item2 = "D-Pad Down";
            Xbox_Item3 = "D-Pad Left";
            Xbox_Item4 = "D-Pad Right";
            Xbox_Inv = "A Button";
            Xbox_Pause = "Start Button";
            Xbox_AttackUse = "Right Trigger";
            Xbox__Aim = "Left Trigger";
        }

    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 400));
        GUI.Box(new Rect(0, 0, 600, 400), "Controls");
        GUI.Label(new Rect(205, 40, 20, 20), "PC");
        GUI.Label(new Rect(340, 40, 125, 20), "Xbox Controller");

        GUI.Label(new Rect(25,75,125,20), "Movement: ");
        GUI.Button(new Rect(150,75,135,20), PC_Move);
        GUI.Button(new Rect(325,75,135,20), Xbox_Move);

        GUI.Label(new Rect(25, 100, 125, 20), "Rotation: ");
        GUI.Button(new Rect(150, 100, 135, 20), PC_Rotate);
        GUI.Button(new Rect(325, 100, 135, 20), Xbox_Rotate);

        GUI.Label(new Rect(25, 125, 125, 20), "Item 1: ");
        GUI.Button(new Rect(150, 125, 135, 20), PC_Item1);
        GUI.Button(new Rect(325, 125, 135, 20), Xbox_Item1);

        GUI.Label(new Rect(25, 150, 125, 20), "Item 2: ");
        GUI.Button(new Rect(150, 150, 135, 20), PC_Item2);
        GUI.Button(new Rect(325, 150, 135, 20), Xbox_Item2);

        GUI.Label(new Rect(25, 175, 125, 20), "Item 3: ");
        GUI.Button(new Rect(150, 175, 135, 20), PC_Item3);
        GUI.Button(new Rect(325, 175, 135, 20), Xbox_Item3);

        GUI.Label(new Rect(25, 200, 125, 20), "Item 4: ");
        GUI.Button(new Rect(150, 200, 135, 20), PC_Item4);
        GUI.Button(new Rect(325, 200, 135, 20), Xbox_Item4);

        GUI.Label(new Rect(25, 225, 125, 20), "Inventory: ");
        GUI.Button(new Rect(150, 225, 135, 20), PC_Inv);
        GUI.Button(new Rect(325, 225, 135, 20), Xbox_Inv);

        GUI.Label(new Rect(25, 250, 125, 20), "Pause Game: ");
        GUI.Button(new Rect(150, 250, 135, 20), PC_Pause);
        GUI.Button(new Rect(325, 250, 135, 20), Xbox_Pause);

        GUI.Label(new Rect(25, 275, 125, 20), "attack/Use: ");
        GUI.Button(new Rect(150, 275, 135, 20), PC_AttackUse);
        GUI.Button(new Rect(325, 275, 135, 20), Xbox_AttackUse);

        GUI.Label(new Rect(25, 300, 125, 20), "Aim: ");
        GUI.Button(new Rect(150, 300, 135, 20), PC_Aim);
        GUI.Button(new Rect(325, 300, 135, 20), Xbox__Aim);

        GUI.Label(new Rect(450,345,125,20), "Current Controls");
        if (GUI.Button(new Rect(425, 370, 135, 20), cProfile.ToString()))
        {
            if (cProfile == ControlProfile.Controller)
                SwitchProfile(ControlProfile.PC);
            else
                SwitchProfile(ControlProfile.Controller);
        }

        GUI.EndGroup();
    }
}
