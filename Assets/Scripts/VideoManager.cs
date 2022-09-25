using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer vid;
    public GuidePageManager guideMng;
    public GameManager gameMng;
    public GameObject   guidePage,
                        canvasCam,
                        lastPage,
                        mapPage,
                        mapScene;

    void Start()
    {
        vid.loopPointReached += CheckOver;
    }
    
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        guideMng.GuidePageSaveData();
        guidePage.SetActive(false);
        canvasCam.SetActive(false);
        lastPage.SetActive(false);
        mapPage.SetActive(true);
        mapScene.SetActive(true);
        gameMng.GotoSungnyemungMap();
    }

    public void VideoPlay()
    {
        vid.Play();
    }
}
