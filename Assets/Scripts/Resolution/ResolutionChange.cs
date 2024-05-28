using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionChange : MonoBehaviour
{

    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    public void HandleResolution(int val)
    {
        switch (val)
        {
            case 0: Screen.SetResolution(1920, 1080, FullScreenMode.Windowed); 
                break;
            case 1: Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
            case 2: Screen.SetResolution(2560, 1440, FullScreenMode.Windowed);
                break;
        }
       
    }
}
