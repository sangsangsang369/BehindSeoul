using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBtn : MonoBehaviour
{
    [SerializeField]
    GameObject mainPage, themeSelectionPage;
    
    public void ThemeSelectionOn()
    {
        mainPage.SetActive(false);
        themeSelectionPage.SetActive(true);
    }
}
