using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;
#if PLATFORM_ANDROID
using UnityEngine.Android;    
#endif

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

                        course_Prefab,
                        hori_Prefab,
                        locker_Prefab,
                        coupon_Prefab,
                        kPrefab,
                        yPrefab,
                        osPrefab,
                        ozPrefab,
                        wPrefab,
                        nPrefab,
                        
                        bagOnlineContent,
                        bagOfflineContent;

    public TMP_Text     hintText;
    public List<GameObject> pages;  
    
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;

    DataManager data;
    SaveDataClass saveData;


    public int nextBtnIndex = 0;

    private void Awake() 
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        #endif
    }
    
    private void Start() 
    {
        data = DataManager.singleTon;
        saveData = data.saveData;

        if(saveData.bagItems.Count > 0)
        {
            foreach(string s in saveData.bagItems)
            {
                if(s == "Hori")
                {
                    GetItemsCouponInBag(hori_Prefab, bagOnlineContent);
                }
                else if(s == "Locker")
                {
                    GetItemsCouponInBag(locker_Prefab, bagOnlineContent);
                }
                else if(s == "Letter_K")
                {
                    GetItemsCouponInBag(kPrefab, bagOnlineContent);
                }
                else if(s == "Letter_Y")
                {
                    GetItemsCouponInBag(yPrefab, bagOnlineContent);
                }
                else if(s == "Letter_O_s")
                {
                    GetItemsCouponInBag(osPrefab, bagOnlineContent);
                }
                else if(s == "Letter_O_z")
                {
                    GetItemsCouponInBag(ozPrefab, bagOnlineContent);
                }
                else if(s == "Letter_W")
                {
                    GetItemsCouponInBag(wPrefab, bagOnlineContent);
                }
                else if(s == "Letter_N")
                {
                    GetItemsCouponInBag(nPrefab, bagOnlineContent);
                }

                
            }
        }
        if(saveData.bagCoupons.Count > 0)
        {
            foreach(string s in saveData.bagCoupons)
            {
                if(s == "One")
                {
                    GetItemsCouponInBag(coupon_Prefab, bagOfflineContent);
                }
            }
        }
        if(saveData.spotCollection.Count > 0)
        {
            foreach(string s in saveData.spotCollection)
            {
                if(s == "숭례문")
                {
                    GetCourseInCollection("숭례문");
                }
                else if(s == "덕수궁")
                {
                    GetCourseInCollection("덕수궁");
                }
            }
        }
    }

    public void GetItemsCouponInBag(GameObject i, GameObject parent)
    {
        GameObject itm = Instantiate(i);
        itm.transform.SetParent(parent.transform, false); 
    }

    public void GetCourseInCollection(string c)
    {
        GameObject course = Instantiate(course_Prefab);
        course.transform.GetChild(0).GetComponent<TMP_Text>().text = c;
        course.transform.SetParent(courseContent.transform, false); 
    }

    public void StartwithSaveData()
    {
        if(saveData.pageChildIndex == 0)
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
            else if(saveData.pagesIndex == 4)
            {
                afterSpotsPage.SetActive(true);
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
        else
        {
            if(saveData.pagesIndex == 0)
            {
                guidePage.SetActive(true);
            }
            else if(saveData.pagesIndex == 1)
            {
                sungPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 2)
            {
                deokPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 3)
            {
                //sungPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 4)
            {                
                afterSpotsPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 5)
            {
                isangPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 6)
            {
                tongInPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 7)
            {
                parkNoSuPage.SetActive(true);
            }
            else if(saveData.pagesIndex == 8)
            {
                yoonPage.SetActive(true);
            }
        }
    }

    public void MapSceneInit()
    {
        mapScene.SetActive(true);
        mapPage.SetActive(true);
        canvasCam.SetActive(false);
    }

    public void GotoSungnyemungMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.559979, 126.9753025";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[0].SetActive(true);
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
        spawnOnMap._spawnedObjects[0].SetActive(false);
        spawnOnMap._spawnedObjects[1].SetActive(false);
        for(int i = 2; i < 8; i++)
        {
            spawnOnMap._spawnedObjects[i].SetActive(true);
        } 
        if(saveData.endedSixSpots.Count > 0 && saveData.endedSixSpots.Count != 6)
        {
            foreach(int e in saveData.endedSixSpots)
            {                    
                spawnOnMap._spawnedObjects[e].transform.GetChild(0).gameObject.SetActive(false);
            }
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
        spawnOnMap._spawnedObjects[0].SetActive(false);
        spawnOnMap._spawnedObjects[8].SetActive(true);
    }

    public void GotoTongInMap()
    {
        abstMap = FindObjectOfType<AbstractMap>();    
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        string locString = "37.58077, 126.97004";
        Vector2d latlon = Conversions.StringToLatLon(locString);
        abstMap.Initialize(latlon, 16);
        spawnOnMap._spawnedObjects[0].SetActive(false);
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
        spawnOnMap._spawnedObjects[0].SetActive(false);
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
        spawnOnMap._spawnedObjects[0].SetActive(false);
        spawnOnMap._spawnedObjects[10].SetActive(false);
        spawnOnMap._spawnedObjects[11].SetActive(true);
    }
}