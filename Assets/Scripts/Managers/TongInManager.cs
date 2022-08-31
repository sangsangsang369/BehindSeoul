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
    TMP_InputField  answerInput;
    [SerializeField]
    GameObject  tongInPage,
                wrongText;
    int currPage = 0;
    GameManager gameMng;

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();     
    }

    public void GoToNextTongInPage()
    {
        tongInPage.transform.GetChild(currPage).gameObject.SetActive(false);
        tongInPage.transform.GetChild(++currPage).gameObject.SetActive(true);
    }

    public void GotoParkNoSuMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.5813, 126.9668";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[9].SetActive(false);
        spawnOnMap._spawnedObjects[10].SetActive(true);
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
}
