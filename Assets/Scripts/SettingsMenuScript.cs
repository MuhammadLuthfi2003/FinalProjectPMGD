using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuScript : MonoBehaviour
{
    public static SettingsMenuScript Instance;

    //Shows the Settings Menu
    public void ShowSettingsMenu()
    {
        gameObject.SetActive(true);
    }

    //Hides the Settings Menu
    public void HideSettingsMenu()
    {
        gameObject.SetActive(false);
    }
}
