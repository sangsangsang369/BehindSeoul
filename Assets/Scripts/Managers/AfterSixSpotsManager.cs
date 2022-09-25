using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;
using TMPro;

public class AfterSixSpotsManager : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    [SerializeField]
    GameObject  afterSixSpotsPage;
    int afterssCurrPage = 0;
    GameManager gameMng;
    [SerializeField]
    List<TMP_Text>  nameContainTexts; 

    DataManager data;
    SaveDataClass saveData;


    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();     
        data = DataManager.singleTon;
        saveData = data.saveData;

        afterssCurrPage = saveData.pageChildIndex;
        afterSixSpotsPage.transform.GetChild(afterssCurrPage).gameObject.SetActive(true);
        foreach (TMP_Text t in nameContainTexts)
        {
            t.text = t.text.Replace("name", ChasaData.chasaName);
        }
    }

    public void GoToNextAfterSixSpotsPage()
    {
        afterSixSpotsPage.transform.GetChild(afterssCurrPage).gameObject.SetActive(false);
        afterSixSpotsPage.transform.GetChild(++afterssCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = afterssCurrPage;
        data.Save();
    }

    public void GoToPrevAfterSixSpotsPage()
    {
        afterSixSpotsPage.transform.GetChild(afterssCurrPage).gameObject.SetActive(false);
        afterSixSpotsPage.transform.GetChild(--afterssCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = afterssCurrPage;
        data.Save();
    }

    public void AddDeokToCollection()
    {
        gameMng.GetCourseInCollection("덕수궁");
        saveData.spotCollection.Add("덕수궁");
        data.Save();
    }

    public void AfterssPageSaveData()
    {
        saveData.pagesIndex = 5;
        saveData.pageChildIndex = 0;
        data.Save();
    }
}
