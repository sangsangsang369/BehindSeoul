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
    AbstractMap abstMap;
    SpawnOnMap spawnOnMap;
    int deokCurrPage = 0;

    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {   
        data = DataManager.singleTon;
        saveData = data.saveData;
        
        deokCurrPage = saveData.pageChildIndex;
        deokPagesParent.transform.GetChild(deokCurrPage).gameObject.SetActive(true);
    }

    public void GoToNextDeokPage()
    {
        deokPagesParent.transform.GetChild(deokCurrPage).gameObject.SetActive(false);
        deokPagesParent.transform.GetChild(++deokCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = deokCurrPage;
        data.Save();
    }

    public void GoToPrevDeokPage()
    {
        deokPagesParent.transform.GetChild(deokCurrPage).gameObject.SetActive(false);
        deokPagesParent.transform.GetChild(--deokCurrPage).gameObject.SetActive(true);
        saveData.pageChildIndex = deokCurrPage;
        data.Save();
    }

    public void DeokPageSaveData()
    {
        saveData.pagesIndex = 3;
        saveData.pageChildIndex = 0;
        data.Save();
    }
}
