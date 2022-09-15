using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;
using TMPro;

public class SixSpotsManager : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    GameManager gMng;
    public int endedSpotsNum = 0;


    void Awake()
    {
        gMng = FindObjectOfType<GameManager>();     
    }
    
    public void EndedSpotOff_Check(int i)
    {
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        spawnOnMap._spawnedObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        endedSpotsNum++;
        
        if(endedSpotsNum == 6)
        {
            gMng.afterSpotsPage.SetActive(true);
        }
    }
}
