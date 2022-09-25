using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JunManager : MonoBehaviour
{
    [SerializeField]
    GameObject  junPage,
                junWrongText,
                wPrefab;

    [SerializeField]
    TMP_InputField  junAnswerInput;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;
    int junCurrPage = 0;

    void Start() 
    {    
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        junPage.transform.GetChild(junCurrPage).gameObject.SetActive(true);
    }

    public void GetWInBag()
    {
        GameObject l = Instantiate(wPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_W");
        data.Save();
    }

    public void GoToNextJunPage()
    {
        junPage.transform.GetChild(junCurrPage).gameObject.SetActive(false);
        junPage.transform.GetChild(++junCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevJunPage()
    {
        junPage.transform.GetChild(junCurrPage).gameObject.SetActive(false);
        junPage.transform.GetChild(--junCurrPage).gameObject.SetActive(true);
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
