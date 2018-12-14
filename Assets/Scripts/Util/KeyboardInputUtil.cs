using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyboardInputUtil {

    #region KEYBOARD
    public static bool IsHoldingKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public static bool KeyWasPressed(KeyCode key)
    {
        return !Input.GetKeyDown(key) && Input.GetKeyUp(key);
    }
    
    public static bool WasAnyKeyPressed()
    {
        List<KeyCode> keys = getAllKeyBoardCodes();
        for (int i = 0; i < keys.Count; i++)
        {
            if (KeyWasPressed(keys[i]))
            {
                return true;
            }
        }
        return false;
    }

    private static List<KeyCode> getAllKeyBoardCodes()
    {
        List<KeyCode> keys = new List<KeyCode>();
        KeyCode[] letterKeys = {KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,
            KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z};
        KeyCode[] numKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
        KeyCode[] numPadKeys = { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.Keypad0, KeyCode.KeypadDivide, KeyCode.KeypadMultiply, KeyCode.KeypadMinus, KeyCode.KeypadPlus, KeyCode.KeypadEnter, KeyCode.KeypadPeriod };
        KeyCode[] functionKeys = { KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12 };
        KeyCode[] specialKeys = { KeyCode.CapsLock,KeyCode.Numlock,KeyCode.LeftControl,KeyCode.LeftApple,KeyCode.LeftAlt,KeyCode.LeftCommand,KeyCode.LeftShift,KeyCode.Tab,KeyCode.Space,
            KeyCode.RightAlt,KeyCode.RightApple,KeyCode.RightCommand,KeyCode.RightControl,KeyCode.RightShift,KeyCode.Return,KeyCode.Backspace};
        KeyCode[] otherKeys = { KeyCode.Semicolon, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.Slash, KeyCode.Backslash, KeyCode.Comma, KeyCode.Period, KeyCode.Equals, KeyCode.Minus };
        foreach (KeyCode item in letterKeys)
        {
            keys.Add(item);
        }
        foreach (KeyCode item in numKeys)
        {
            keys.Add(item);
        }
        foreach (KeyCode item in numPadKeys)
        {
            keys.Add(item);
        }
        foreach (KeyCode item in functionKeys)
        {
            keys.Add(item);
        }
        foreach (KeyCode item in specialKeys)
        {
            keys.Add(item);
        }
        foreach (KeyCode item in otherKeys)
        {
            keys.Add(item);
        }
        return keys;
    }
    #endregion

}
