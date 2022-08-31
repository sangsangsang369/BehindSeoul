using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class DeoksugungManager : MonoBehaviour
{
    [SerializeField]
    GameObject  deokPagesParent;
    int currPage = 0;
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    


    public void GoToNextDeokPage()
    {
        deokPagesParent.transform.GetChild(currPage).gameObject.SetActive(false);
        deokPagesParent.transform.GetChild(++currPage).gameObject.SetActive(true);
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
}
