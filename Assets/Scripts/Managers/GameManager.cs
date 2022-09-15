using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class GameManager : MonoBehaviour
{
    public GameObject   guidePage,
                        sungPage,
                        deokPage,
                        gwangPage,
                        hamPage,
                        seokPage,
                        zukPage,
                        junPage,
                        jungPage,
                        afterSpotsPage,
                        isangPage,
                        tongInPage,
                        parkNoSuPage,
                        yoonPage,
                        canvasCam,
                        mapScene,
                        mapPage,
                        upperBarBtns,
                        hintBtn,
                        whenImgTracked,
                        SuccessPopup,
                        courseContent,
                        coursePrefab;
    public TMP_Text     hintText;
    public List<GameObject> pages;  
    
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;

    DataManager data;
    SaveDataClass saveData;


    public int nextBtnIndex = 0;

    private void Start() 
    {
        data = DataManager.singleTon;
        saveData = data.saveData;
    }

    public void GetCourseInCollection(string c)
    {
        GameObject course = Instantiate(coursePrefab);
        course.transform.GetChild(0).GetComponent<TMP_Text>().text = c;
        course.transform.SetParent(courseContent.transform, false); 
    }

    public void StartwithSaveData()
    {
        if(saveData.pagesIndex == 0)
        {
            guidePage.SetActive(true);
        }
        else if(saveData.pagesIndex == 1)
        {
            MapSceneInit();
        }
        else if(saveData.pagesIndex == 2)
        {
            MapSceneInit();
            GotoDeoksugungMap();
        }
        else if(saveData.pagesIndex == 3)
        {
            MapSceneInit();
            GotoSixSpotsMap();
        }
        else if(saveData.pagesIndex == 5)
        {
            MapSceneInit();
            GotoIsangHomeMap();
        }
        else if(saveData.pagesIndex == 6)
        {
            MapSceneInit();
            GotoTongInMap();
        }
        else if(saveData.pagesIndex == 7)
        {
            MapSceneInit();
            GotoParkNoSuMap();
        }
        else if(saveData.pagesIndex == 8)
        {
            MapSceneInit();
            GotoYoonDongJuMap();
        }
    }

    public void MapSceneInit()
    {
        mapScene.SetActive(true);
        mapPage.SetActive(true);
        canvasCam.SetActive(false);
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

    public void GotoSixSpotsMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.565913, 126.975286";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[1].SetActive(false);
        for(int i = 2; i < 8; i++)
        {
            spawnOnMap._spawnedObjects[i].SetActive(true);
        } 
    }

    public void GotoIsangHomeMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.5786, 126.9709";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        for(int i = 2; i < 8; i++)
        {
            spawnOnMap._spawnedObjects[i].SetActive(false);
        }
        spawnOnMap._spawnedObjects[8].SetActive(true);
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

    public void GotoYoonDongJuMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.5814, 126.9657";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 17);
        spawnOnMap._spawnedObjects[10].SetActive(false);
        spawnOnMap._spawnedObjects[11].SetActive(true);
    }
}