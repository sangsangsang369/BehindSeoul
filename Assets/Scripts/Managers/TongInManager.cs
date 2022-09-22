using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class TongInManager : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    [SerializeField]
    TMP_InputField  answerInput,
                    answer2Input;
    [SerializeField]
    GameObject  tongInPage,
                wrongText,
                wrongText2;
    GameManager gameMng;
    public int tonginCurrPage = 0;

    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();   
        data = DataManager.singleTon;
        saveData = data.saveData; 
        tongInPage.transform.GetChild(tonginCurrPage).gameObject.SetActive(true); 
    }

    public void GoToNextTongInPage()
    {
        tongInPage.transform.GetChild(tonginCurrPage).gameObject.SetActive(false);
        tongInPage.transform.GetChild(++tonginCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevTongInPage()
    {
        tongInPage.transform.GetChild(tonginCurrPage).gameObject.SetActive(false);
        tongInPage.transform.GetChild(--tonginCurrPage).gameObject.SetActive(true);
    }

    public void AnswerSubmitBtnFunc()
    {
        wrongText.SetActive(false);
        if(answerInput.text != "도깨비 부부")
        {
            wrongText.SetActive(true);
        }
        else
        {
            GoToNextTongInPage();
            gameMng.hintBtn.SetActive(false);
        }
    }

    public void Answer2SubmitBtnFunc()
    {
        wrongText2.SetActive(false);
        if(answer2Input.text == "새" || answer2Input.text == "까치")
        {
            GoToNextTongInPage();
        }
        else
        {
            wrongText2.SetActive(true);
        }
    }

    public void TonginPageSaveData()
    {
        saveData.pagesIndex = 7;
        data.Save();
    }
}
