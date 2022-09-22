using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GwangManager : MonoBehaviour
{
    [SerializeField]
    GameObject  gwangPage,
                gwangWrongText;

    [SerializeField]
    TMP_InputField  gwangAnswerInput;
    GameManager gMng;
    int gwangCurrPage = 0;

    void Start() 
    {   
        gMng = FindObjectOfType<GameManager>();  
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextGwangPage()
    {
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(false);
        gwangPage.transform.GetChild(++gwangCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevGwangPage()
    {
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(false);
        gwangPage.transform.GetChild(--gwangCurrPage).gameObject.SetActive(true);
    }

    public void GwangAnswerSubmitBtnFunc()
    {
        gwangWrongText.SetActive(false);
        if(gwangAnswerInput.text != "1904")
        {
            gwangWrongText.SetActive(true);
        }
        else
        {
            gMng.hintBtn.SetActive(false);
            GoToNextGwangPage();
        }
    }
}
