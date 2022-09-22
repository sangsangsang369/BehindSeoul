using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HamManager : MonoBehaviour
{
    [SerializeField]
    GameObject  hamPage,
                hamWrongText;

    [SerializeField]
    TMP_InputField  hamAnswerInput;
    int hamCurrPage = 0;

    void Start() 
    {    
        hamPage.transform.GetChild(hamCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextHamPage()
    {
        hamPage.transform.GetChild(hamCurrPage).gameObject.SetActive(false);
        hamPage.transform.GetChild(++hamCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevHamPage()
    {
        hamPage.transform.GetChild(hamCurrPage).gameObject.SetActive(false);
        hamPage.transform.GetChild(--hamCurrPage).gameObject.SetActive(true);
    }

    public void HamAnswerSubmitBtnFunc()
    {
        hamWrongText.SetActive(false);
        if(hamAnswerInput.text != "온돌")
        {
            hamWrongText.SetActive(true);
        }
        else
        {
            GoToNextHamPage();
        }
    }
}
