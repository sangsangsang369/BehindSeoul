using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;


public class SungnyemunManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField  answer1Input,
                    answer2Input;

    [SerializeField]
    GameObject  sungPagesParent,
                bagOfflineContent,
                couponPrefab,
                wrong1Text,
                wrong2Text,
                popup;
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    GameManager gameMng;
    [SerializeField]
    List<TMP_Text>  nameContainTexts; 
    int sungCurrPage = 0;
    
    DataManager data;
    SaveDataClass saveData;
    

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();    
        data = DataManager.singleTon;
        saveData = data.saveData;

        sungPagesParent.transform.GetChild(sungCurrPage).gameObject.SetActive(true);
        foreach (TMP_Text t in nameContainTexts)
        {
            t.text = t.text.Replace("name", ChasaData.chasaName);
        }
    }

    public void GoToNextSungPage()
    {
        sungPagesParent.transform.GetChild(sungCurrPage).gameObject.SetActive(false);
        sungPagesParent.transform.GetChild(++sungCurrPage).gameObject.SetActive(true);
    }

    public void Answer1SubmitBtnFunc()
    {
        wrong1Text.SetActive(false);
        if(answer1Input.text != "수류견")
        {
            wrong1Text.SetActive(true);
        }
        else
        {
            GoToNextSungPage();
        }
    }

    public void Answer2SubmitBtnFunc()
    {
        wrong2Text.SetActive(false);
        if(answer2Input.text != "덕수궁의불")
        {
            wrong2Text.SetActive(true);
        }
        else
        {
            GetCouponInBag(couponPrefab);
            popup.SetActive(true);
            gameMng.GetCourseInCollection("숭례문");
            gameMng.hintBtn.SetActive(false);
        }
    }

    public void GetCouponInBag(GameObject prefab)
    {
        GameObject locker = Instantiate(prefab);
        locker.transform.SetParent(bagOfflineContent.transform, false); 
    }

    public void SungPageSaveData()
    {
        saveData.pagesIndex = 2;
        data.Save();
    }
}
