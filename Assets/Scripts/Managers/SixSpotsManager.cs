using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class SixSpotsManager : MonoBehaviour
{
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    [SerializeField]
    GameObject afterSixSpotsPage;
    int currPage = 0;
    
    public void GoToNextAfterSixSpotsPage()
    {
        afterSixSpotsPage.transform.GetChild(currPage).gameObject.SetActive(false);
        afterSixSpotsPage.transform.GetChild(++currPage).gameObject.SetActive(true);
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
}
