using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HamManager : MonoBehaviour
{
    [SerializeField]
    GameObject  hamPage,
                hamWrongText,
                yPrefab;

    [SerializeField]
    TMP_InputField  hamAnswerInput;
    int hamCurrPage = 0;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {    
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        hamPage.transform.GetChild(hamCurrPage).gameObject.SetActive(true);
    }

    public void GetYInBag()
    {
        GameObject l = Instantiate(yPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_Y");
        data.Save();
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
