using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;


public class IsangHomePage : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    [SerializeField]
    TMP_InputField  answerInput;
    [SerializeField]
    GameObject isangHomePage,
                wrongText;
    [SerializeField]
    List<TMP_Text>  nameContainTexts; 
    int isangCurrPage = 0;
    DataManager data;
    SaveDataClass saveData;
    
    void Start() 
    {   
        data = DataManager.singleTon;
        saveData = data.saveData;
        isangHomePage.transform.GetChild(isangCurrPage).gameObject.SetActive(true);
        foreach (TMP_Text t in nameContainTexts)
        {
            t.text = t.text.Replace("name", ChasaData.chasaName);
        }
    }

    public void GoToNextIsangHomePage()
    {
        isangHomePage.transform.GetChild(isangCurrPage).gameObject.SetActive(false);
        isangHomePage.transform.GetChild(++isangCurrPage).gameObject.SetActive(true);
    }

    public void AnswerSubmitBtnFunc()
    {
        wrongText.SetActive(false);
        if(answerInput.text != "시장")
        {
            wrongText.SetActive(true);
        }
        else
        {
            GoToNextIsangHomePage();
        }
    }

    public void IsangPageSaveData()
    {
        saveData.pagesIndex = 6;
        data.Save();
    }
}
