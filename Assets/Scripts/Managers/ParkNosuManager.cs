using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class ParkNosuManager : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    [SerializeField]
    TMP_InputField  answerInput;
    [SerializeField]
    GameObject  nosuPage,
                wrongText;
    public int nosuCurrPage = 0;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();    
        data = DataManager.singleTon;
        saveData = data.saveData; 

        nosuCurrPage = saveData.pageChildIndex;
        nosuPage.transform.GetChild(nosuCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextNosuPage()
    {
        nosuPage.transform.GetChild(nosuCurrPage).gameObject.SetActive(false);
        nosuPage.transform.GetChild(++nosuCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = nosuCurrPage;
        data.Save();
    }

    public void GoToPrevNosuPage()
    {
        nosuPage.transform.GetChild(nosuCurrPage).gameObject.SetActive(false);
        nosuPage.transform.GetChild(--nosuCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = nosuCurrPage;
        data.Save();
    }

    public void AnswerSubmitBtnFunc()
    {
        wrongText.SetActive(false);
        if(answerInput.text != "1917")
        {
            wrongText.SetActive(true);
        }
        else
        {
            GoToNextNosuPage();
        }
    }

    public void NosuPageSaveData()
    {
        saveData.pagesIndex = 8;
        saveData.pageChildIndex = 0;
        data.Save();
    }
}
