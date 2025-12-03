using Code.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Code
{
    public class KeyboardHandler : MonoBehaviour
    {
        private void Update()
        {
            PressKeyboard();
        }

        private void PressKeyboard()
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key != null && key.wasPressedThisFrame)
                {
                    string keyName = key.displayName;
                    if (keyName.Length == 1)
                    {
                        char letter = char.ToLower(keyName[0]);
                        if(char.IsDigit(letter))
                        {
                            continue;
                        }
                        new OnKeyboardPressedEvent(letter).Publish(this);
                    }
                }
            }
        }
    }
}