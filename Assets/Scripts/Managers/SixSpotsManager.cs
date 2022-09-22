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
    public int endedSpotsNum = 0;

    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;
    
    private void Awake() 
    {
        gameMng = FindObjectOfType<GameManager>(); 
        data = DataManager.singleTon;
        saveData = data.saveData;
    }
    
    public void EndedSpotOff_Check(int i)
    {
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
        spawnOnMap._spawnedObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        endedSpotsNum++;

        saveData.endedSixSpots.Add(i);
        data.Save();

        if(endedSpotsNum == 6 || saveData.endedSixSpots.Count == 6)
        {
            gameMng.afterSpotsPage.SetActive(true);
            saveData.pagesIndex = 4;
            data.Save();
        }
    }
}
