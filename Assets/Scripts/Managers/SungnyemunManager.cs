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
    TMP_Text    textOne; 

    [SerializeField]
    GameObject  sungPagesParent,
                bagOfflineContent,
                couponPrefab,
                wrong1Text,
                wrong2Text;
    int currPage = 0;
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    GameManager gameMng;

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();     
    }


    public void GoToNextSungPage()
    {
        sungPagesParent.transform.GetChild(currPage).gameObject.SetActive(false);
        sungPagesParent.transform.GetChild(++currPage).gameObject.SetActive(true);
    }

    public void TextRepalceToChasaName()
    {
        textOne.text = textOne.text.Replace("name", ChasaData.chasaName);
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
            GoToNextSungPage();
            gameMng.hintBtn.SetActive(false);
        }
    }

    public void GetCouponInBag(GameObject prefab)
    {
        GameObject locker = Instantiate(prefab);
        locker.transform.SetParent(bagOfflineContent.transform, false); 
    }

    public void GotoDeoksugungMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.566015, 126.9750175";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[0].SetActive(false);
        spawnOnMap._spawnedObjects[1].SetActive(true);
    }
}
