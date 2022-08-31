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
    int currPage = 0;
    
    public void GoToNextIsangHomePage()
    {
        isangHomePage.transform.GetChild(currPage).gameObject.SetActive(false);
        isangHomePage.transform.GetChild(++currPage).gameObject.SetActive(true);
    }

    public void GotoTongInMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.58077, 126.97004";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[8].SetActive(false);
        spawnOnMap._spawnedObjects[9].SetActive(true);
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
}
