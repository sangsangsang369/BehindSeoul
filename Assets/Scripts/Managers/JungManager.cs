using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JungManager : MonoBehaviour
{
    [SerializeField]
    GameObject  jungPage,
                jungWrongText;

    [SerializeField]
    TMP_InputField  jungAnswerInput;
    GameManager gMng;
    int jungCurrPage = 0;

    void Start() 
    {     
        gMng = FindObjectOfType<GameManager>();  
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextJungPage()
    {
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(false);
        jungPage.transform.GetChild(++jungCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevJungPage()
    {
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(false);
        jungPage.transform.GetChild(--jungCurrPage).gameObject.SetActive(true);
    }

    public void JungAnswerSubmitBtnFunc()
    {
        jungWrongText.SetActive(false);
        if(jungAnswerInput.text != "드므")
        {
            jungWrongText.SetActive(true);
        }
        else
        {
            gMng.hintBtn.SetActive(false);
            GoToNextJungPage();
        }
    }
}
