using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class Spots : MonoBehaviour
{
    GameManager gameMng;
    SpawnOnMap spawnOnMap;

    private void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();      
        spawnOnMap = FindObjectOfType<SpawnOnMap>();
    }

    public void StartSungPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.sungPage.SetActive(true);
    }
    public void StartDeokPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.deokPage.SetActive(true);
    }
    
    public void StartGwangPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.gwangPage.SetActive(true);
    }
    public void StartHamPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.hamPage.SetActive(true);
    }
    public void StartSeokPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.seokPage.SetActive(true);
    }
    public void StartZukPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.zukPage.SetActive(true);
    }
    public void StartJunPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.junPage.SetActive(true);
    }
    public void StartJungPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.jungPage.SetActive(true);
    }
    public void StartIsangPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.isangPage.SetActive(true);
    }
    public void StartTongInPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.tongInPage.SetActive(true);
    }
    public void StartParkNoSuPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.parkNoSuPage.SetActive(true);
    }
    public void StartYoonDongJuPage()
    {
        gameMng.canvasCam.SetActive(true);
        gameMng.mapScene.SetActive(false);
        gameMng.yoonPage.SetActive(true);
    }

    public void RestSpotsOff()
    {
        for (int i = 2; i < 8; i++)
        {
            if(spawnOnMap._spawnedObjects[i] != this.gameObject)
            {
                spawnOnMap._spawnedObjects[i].SetActive(false);
            }
        }
    }

    public void RestSpotsOn()
    {
        for (int i = 2; i < 8; i++)
        {
            if(spawnOnMap._spawnedObjects[i] != this.gameObject)
            {
                spawnOnMap._spawnedObjects[i].SetActive(true);
            }
        }
    }
    
}
