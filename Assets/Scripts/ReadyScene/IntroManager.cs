using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    GameObject intro;
    [SerializeField]
    GameObject login_Enter;
   

    private void Start() 
    {
        videoPlayer.loopPointReached += IntroEnded;
        videoPlayer.Play();   
    }

    void IntroEnded(VideoPlayer vp)
    {
        StopIntroVideo();
    }

    public void StopIntroVideo()
    {
        intro.SetActive(false);
        videoPlayer.Stop();
        login_Enter.SetActive(true);
    }

}
