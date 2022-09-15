using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeokManager : MonoBehaviour
{
    [SerializeField]
    GameObject  seokPage,
                seokWrongText;

    [SerializeField]
    TMP_InputField  seokAnswerInput;
    int seokCurrPage = 0;

    void Start() 
    {    
        seokPage.transform.GetChild(seokCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextSeokPage()
    {
        seokPage.transform.GetChild(seokCurrPage).gameObject.SetActive(false);
        seokPage.transform.GetChild(++seokCurrPage).gameObject.SetActive(true);
    }

    public void SeokAnswerSubmitBtnFunc()
    {
        seokWrongText.SetActive(false);
        if(seokAnswerInput.text != "1" && seokAnswerInput.text != "1번")
        {
            seokWrongText.SetActive(true);
        }
        else
        {
            GoToNextSeokPage();
        }
    }
}
