using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    /*KeyCode[] values = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
    static Dictionary<string, string> keyMapping;

    static string[] keyMaps = new string[10]
    {
        "Attack_1",
        "Forward_1",
        "Backward_1",
        "Left_1",
        "Right_1",
        "Attack_2",
        "Forward_2",
        "Backward_2",
        "Left_2",
        "Right_2"
    };
    static string[] defaults = new string[10];

    static ControlsManager()
    {
        InitializeKeyCodes();
        InitializeDictionary();
    }

    private static void InitializeKeyCodes()
    {
        if (PlayerPrefs.HasKey("Attack_1"))
        {
            for (int i = 0; i < 9; i++)
            {
                defaults[i] = PlayerPrefs.GetString(keyMaps[i]);
            }
        }
        else
        {
            defaults = new string[10]
            {
                KeyCode.Mouse0.ToString(),
                KeyCode.UpArrow.ToString(),
                KeyCode.DownArrow.ToString(),
                KeyCode.LeftArrow.ToString(),
                KeyCode.RightArrow.ToString(),
                KeyCode.Space.ToString(),
                KeyCode.W.ToString(),
                KeyCode.S.ToString(),
                KeyCode.A.ToString(),
                KeyCode.D.ToString()
            };
        }
    }
    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, string>();
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }

    public static bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(keyMapping[keyMap]);
    }
    public void SetKey(string keyMap)
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode value in values)
            {
                if (Input.GetKeyDown(value))
                {
                    if (!keyMapping.ContainsKey(keyMap))
                        Debug.LogError("Given KeyMap string not found");
                    else
                        keyMapping[keyMap] = value.ToString();
                }
            }
        }
    }*/
}
