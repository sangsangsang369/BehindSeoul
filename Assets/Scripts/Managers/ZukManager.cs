using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZukManager : MonoBehaviour
{
    [SerializeField]
    GameObject  zukPage,
                seokWrongText;

    [SerializeField]
    TMP_InputField  seokAnswerInput;
    int zukCurrPage = 0;

    void Start() 
    {  
        zukPage.transform.GetChild(zukCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextZukPage()
    {
        zukPage.transform.GetChild(zukCurrPage).gameObject.SetActive(false);
        zukPage.transform.GetChild(++zukCurrPage).gameObject.SetActive(true);
    }
}
