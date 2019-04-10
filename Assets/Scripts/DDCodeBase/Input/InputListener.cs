using UnityEngine;
using System.Collections;

public class InputListener : MonoSingleton<InputListener>
{
    public delegate void HandleKeyboardInput(KeyCode key);
    public static event HandleKeyboardInput EventKeyboardInput;

    private KeyCode currentKey;

    void Start()
    {
        currentKey = KeyCode.Space;
    }

    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                currentKey = e.keyCode;

                if (currentKey.ToString().Equals("None"))
                    return;

                EventKeyboardInput(currentKey);
                //Debuger.Log("Current Key is : " + currentKey.ToString());
            }
        }
    }
}
