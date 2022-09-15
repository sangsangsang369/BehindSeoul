using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JunManager : MonoBehaviour
{
    [SerializeField]
    GameObject  junPage,
                junWrongText;

    [SerializeField]
    TMP_InputField  junAnswerInput;
    int junCurrPage = 0;

    void Start() 
    {    
        junPage.transform.GetChild(junCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextJunPage()
    {
        junPage.transform.GetChild(junCurrPage).gameObject.SetActive(false);
        junPage.transform.GetChild(++junCurrPage).gameObject.SetActive(true);
    }

    public void JunAnswerSubmitBtnFunc()
    {
        junWrongText.SetActive(false);
        if(junAnswerInput.text != "3" && junAnswerInput.text != "3번" &&junAnswerInput.text != "연꽃")
        {
            junWrongText.SetActive(true);
        }
        else
        {
            GoToNextJunPage();
        }
    }
}
