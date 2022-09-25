using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeokManager : MonoBehaviour
{
    [SerializeField]
    GameObject  seokPage,
                seokWrongText,
                o_sPrefab;

    [SerializeField]
    TMP_InputField  seokAnswerInput;
    int seokCurrPage = 0;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {    
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        seokPage.transform.GetChild(seokCurrPage).gameObject.SetActive(true);
    }

    public void GetOsInBag()
    {
        GameObject l = Instantiate(o_sPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_O_s");
        data.Save();
    }

    public void GoToNextSeokPage()
    {
        seokPage.transform.GetChild(seokCurrPage).gameObject.SetActive(false);
        seokPage.transform.GetChild(++seokCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevSeokPage()
    {
        seokPage.transform.GetChild(seokCurrPage).gameObject.SetActive(false);
        seokPage.transform.GetChild(--seokCurrPage).gameObject.SetActive(true);
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
